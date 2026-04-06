#!/usr/bin/env bash
set -euo pipefail

usage() {
  cat <<'EOF'
Usage:
  ./DataLayer/release.sh <version> [--push] [--source <NuGetSourceName>] [--allow-dirty] [--allow-non-main] [--allow-missing-tag]

Examples:
  ./DataLayer/release.sh 1.0.85
  ./DataLayer/release.sh 1.0.85 --push
  ./DataLayer/release.sh 1.0.85 --push --source MyGitHub
  ./DataLayer/release.sh 1.0.85 --allow-dirty
  ./DataLayer/release.sh 1.0.85 --allow-non-main
  ./DataLayer/release.sh 1.0.85 --push --allow-missing-tag

Notes:
  - Updates version in DataLayer/DataLayer.csproj and DataLayer/DataLayer.nuspec.
  - Runs dotnet clean + dotnet pack -c Release.
  - Push is optional; requires GITHUB_TOKEN when --push is used.
  - Release is blocked if git has uncommitted changes, unless --allow-dirty is passed.
  - Release is blocked outside branch 'main', unless --allow-non-main is passed.
  - Push requires a git tag named v<version> (example: v1.0.85), unless --allow-missing-tag is passed.
EOF
}

if [[ $# -lt 1 ]]; then
  usage
  exit 1
fi

if [[ "${1:-}" == "-h" || "${1:-}" == "--help" ]]; then
  usage
  exit 0
fi

VERSION="$1"
shift

PUSH=false
SOURCE="MyGitHub"
ALLOW_DIRTY=false
ALLOW_NON_MAIN=false
ALLOW_MISSING_TAG=false

while [[ $# -gt 0 ]]; do
  case "$1" in
    --push)
      PUSH=true
      shift
      ;;
    --source)
      if [[ $# -lt 2 ]]; then
        echo "Error: --source requires a value." >&2
        exit 1
      fi
      SOURCE="$2"
      shift 2
      ;;
    --allow-dirty)
      ALLOW_DIRTY=true
      shift
      ;;
    --allow-non-main)
      ALLOW_NON_MAIN=true
      shift
      ;;
    --allow-missing-tag)
      ALLOW_MISSING_TAG=true
      shift
      ;;
    -h|--help)
      usage
      exit 0
      ;;
    *)
      echo "Error: Unknown argument '$1'." >&2
      usage
      exit 1
      ;;
  esac
done

if ! [[ "$VERSION" =~ ^[0-9]+\.[0-9]+\.[0-9]+([.-][0-9A-Za-z.-]+)?$ ]]; then
  echo "Error: Version '$VERSION' is not valid SemVer (example: 1.0.85 or 1.0.85-beta.1)." >&2
  exit 1
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
PROJECT_FILE="$REPO_ROOT/DataLayer/DataLayer.csproj"
NUSPEC_FILE="$REPO_ROOT/DataLayer/DataLayer.nuspec"
NUGET_CONFIG="$REPO_ROOT/DataLayer/nuget.config"

if [[ ! -f "$PROJECT_FILE" ]]; then
  echo "Error: Missing project file: $PROJECT_FILE" >&2
  exit 1
fi

if [[ ! -f "$NUSPEC_FILE" ]]; then
  echo "Error: Missing nuspec file: $NUSPEC_FILE" >&2
  exit 1
fi

if [[ ! -f "$NUGET_CONFIG" ]]; then
  echo "Error: Missing NuGet config: $NUGET_CONFIG" >&2
  exit 1
fi

if [[ "$ALLOW_NON_MAIN" != true ]]; then
  CURRENT_BRANCH="$(git -C "$REPO_ROOT" rev-parse --abbrev-ref HEAD)"
  if [[ "$CURRENT_BRANCH" != "main" ]]; then
    echo "Error: Current branch is '$CURRENT_BRANCH'. Release is only allowed from 'main', or pass --allow-non-main." >&2
    exit 1
  fi
fi

if [[ "$ALLOW_DIRTY" != true ]]; then
  if [[ -n "$(git -C "$REPO_ROOT" status --porcelain)" ]]; then
    echo "Error: Git working tree has uncommitted changes. Commit/stash first, or pass --allow-dirty." >&2
    git -C "$REPO_ROOT" status --short
    exit 1
  fi
fi

if [[ "$PUSH" == true && "$ALLOW_MISSING_TAG" != true ]]; then
  REQUIRED_TAG="v$VERSION"
  if ! git -C "$REPO_ROOT" rev-parse -q --verify "refs/tags/$REQUIRED_TAG" >/dev/null; then
    echo "Error: Missing required tag '$REQUIRED_TAG'. Create it first, or pass --allow-missing-tag." >&2
    echo "Hint: git tag $REQUIRED_TAG && git push origin $REQUIRED_TAG" >&2
    exit 1
  fi
fi

echo "Setting package version to $VERSION"
perl -i -pe 's|<Version>[^<]+</Version>|<Version>'"$VERSION"'</Version>|g' "$PROJECT_FILE"
perl -i -pe 's|<version>[^<]+</version>|<version>'"$VERSION"'</version>|g' "$NUSPEC_FILE"

echo "Packing DataLayer $VERSION"
dotnet clean "$PROJECT_FILE"
dotnet pack "$PROJECT_FILE" -c Release

PACKAGE_PATH="$REPO_ROOT/DataLayer/bin/Release/sanddata.no.common.datalayer.$VERSION.nupkg"
if [[ ! -f "$PACKAGE_PATH" ]]; then
  echo "Error: Package not found: $PACKAGE_PATH" >&2
  exit 1
fi

echo "Created package: $PACKAGE_PATH"

if [[ "$PUSH" == true ]]; then
  if [[ -z "${GITHUB_TOKEN:-}" ]]; then
    echo "Error: GITHUB_TOKEN is required when using --push." >&2
    exit 1
  fi

  echo "Pushing package to source '$SOURCE'"
  dotnet nuget push "$PACKAGE_PATH" \
    --source "$SOURCE" \
    --api-key "$GITHUB_TOKEN" \
    --configfile "$NUGET_CONFIG" \
    --skip-duplicate

  echo "Push complete."
else
  echo "Push skipped. Use --push to publish this package."
fi

