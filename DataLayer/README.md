## DataLayer Package Release

This project publishes the NuGet package `sanddata.no.common.datalayer` to GitHub Packages.

## Quick release (recommended)

Use the script `DataLayer/release.sh` from repository root.

```bash
cd "/Users/roysand/develop/repo/ams/sanddata.no.common"

# Bump version + clean + pack (no publish)
./DataLayer/release.sh 1.0.85

# Create and push release tag before publish
git tag v1.0.85
git push origin v1.0.85

# Bump version + clean + pack + publish
export GITHUB_TOKEN="<YOUR_GITHUB_PAT>"
./DataLayer/release.sh 1.0.85 --push
```

By default, the script blocks release if:
- the git working tree has uncommitted changes
- the current branch is not `main`
- `--push` is used and tag `v<version>` does not exist

Use these overrides only when intentional:

```bash
./DataLayer/release.sh 1.0.85 --allow-dirty
./DataLayer/release.sh 1.0.85 --allow-non-main
./DataLayer/release.sh 1.0.85 --push --allow-missing-tag
./DataLayer/release.sh 1.0.85 --allow-dirty --allow-non-main --push --allow-missing-tag
```

Optional: publish to another configured source name.

```bash
./DataLayer/release.sh 1.0.85 --push --source MyGitHub
```

## What the script does

1. Verifies current branch is `main` (unless `--allow-non-main` is passed).
2. Verifies git working tree is clean (unless `--allow-dirty` is passed).
3. Updates version in `DataLayer/DataLayer.csproj`.
4. Updates version in `DataLayer/DataLayer.nuspec`.
5. Runs `dotnet clean` + `dotnet pack -c Release`.
6. When `--push` is used, verifies tag `v<version>` exists (unless `--allow-missing-tag` is passed).
7. Optionally runs `dotnet nuget push` with `--configfile DataLayer/nuget.config`.

## Manual publish (fallback)

```bash
cd "/Users/roysand/develop/repo/ams/sanddata.no.common"

dotnet clean "DataLayer/DataLayer.csproj"
dotnet pack "DataLayer/DataLayer.csproj" -c Release

git tag "v<NEW_VERSION>"
git push origin "v<NEW_VERSION>"

export GITHUB_TOKEN="<YOUR_GITHUB_PAT>"

dotnet nuget push "DataLayer/bin/Release/sanddata.no.common.datalayer.<NEW_VERSION>.nupkg" \
  --source "MyGitHub" \
  --api-key "$GITHUB_TOKEN" \
  --configfile "DataLayer/nuget.config" \
  --skip-duplicate
```

## Verify

```bash
dotnet nuget list source --configfile "DataLayer/nuget.config"
ls -la "DataLayer/bin/Release" | grep "sanddata.no.common.datalayer"
git tag -l "v*" | tail -n 10
```

Then confirm the new version in GitHub:
- Repository/Owner -> **Packages** -> `sanddata.no.common.datalayer`

## Notes

- `DataLayer/nuget.config` defines `MyGitHub` as `https://nuget.pkg.github.com/roysand/index.json`.
- If you run publish commands from another folder, always pass `--configfile`.
- If a version already exists, `--skip-duplicate` avoids failure.
