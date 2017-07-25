
function SetAssemblyInfo([string]$sourceDirectory, [string]$version) {

    Write-Host "Appling version" $version
    $versionRegex = "\d+.\d+.\d+"
    $versionData = [regex]::matches($version,$versionRegex)

    if ($versionData.Count -eq 0) { Throw "Version " + $version + " has a bad format" }
    
    $files = Get-ChildItem $sourceDirectory -recurse -include "*Properties*" | 
                ?{ $_.PSIsContainer } | 
                foreach { Get-ChildItem -Path $_.FullName -Recurse -include AssemblyInfo.* }

    if ($files) {
        Write-Host "Updating version" $version
        foreach ($file in $files) {
            $filecontent = Get-Content($file)
            attrib $file -r
            $informationalVersion = [regex]::matches($filecontent,"AssemblyInformationalVersion\(""$version""\)")

            if ($informationalVersion.Count -eq 0) {
                Write-Host "Version " $version " applied to " $file
                $filecontent -replace "AssemblyInformationalVersion\(.*\)", "AssemblyInformationalVersion(""$version"")" | Out-File $file
            }

        }
    }
}

$msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\msbuild.exe"
$solutionFolder = $PSScriptRoot + "..\..\"
$solutionFile = $solutionFolder + "\EFContextMock.sln"
$projectFile = $solutionFolder + "\DALTest\DAL.Test.csproj"
$nuget = $solutionFolder + "\nuget\nuget.exe"
$options = "/p:Configuration=Release"
$version = $(git describe --abbrev=0 --tag)

SetAssemblyInfo $solutionFolder $version

Write-Host "Restore packages"

& $nuget restore $solutionFile

if ($LastExitCode -ne 0){
    $exitCode=$LastExitCode
    Write-Error "Build failed!"
    exit $exitCode
}
else{
    Write-Host "Build succeeded"
}

Write-Host "Building"

& $msbuild $projectFile $options

if ($LastExitCode -ne 0){
    $exitCode=$LastExitCode
    Write-Error "Build failed!"
    exit $exitCode
}
else{
    Write-Host "Build succeeded"
}