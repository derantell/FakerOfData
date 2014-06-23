# Create a build tag, formatted like {build}.{branch}.{sha7}
$sha = "$env:APPVEYOR_REPO_COMMIT".substring(0,7)
$env:MY_BUILD_TAG = "$env:APPVEYOR_BUILD_NUMBER.$env:APPVEYOR_REPO_BRANCH.$sha"
# Use aver to patch versions of assemblies and nupkg-packages
.\tools\aver.exe set "$($env:APPVEYOR_BUILD_FOLDER)" -scan -build "$env:MY_BUILD_TAG" -verbose

# Set Appveyor build info to look like we want it
# Use version from root version.txt file 
if(Test-Path .\version.txt) {
    # Convention: first line of version.txt is semver version
    $version = (Get-Content .\version.txt -Head 1).Trim()
    # Use AppVeyor Powershell API to set version+buildtag
    Update-AppveyorBuild -Version "$version+$env:MY_BUILD_TAG"
}
