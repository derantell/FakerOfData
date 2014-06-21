# Create a build tag, formatted like {build}.{branch}.{sha7}
# Use aver to patch versions of assemblies and nupkg-packages
$sha = "$env:APPVEYOR_REPO_COMMIT".substring(0,7)
$env:MY_BUILD_TAG = "$env:APPVEYOR_BUILD_NUMBER.$env:APPVEYOR_REPO_BRANCH.$sha"
.\tools\aver.exe set "$($env:APPVEYOR_BUILD_FOLDER)" -scan -build "$env:MY_BUILD_TAG" -verbose
