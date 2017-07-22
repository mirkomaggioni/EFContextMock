$msbuild = "C:\Program Files (x86)\MSBuild\14.0\bin\amd64\msbuild.exe"
$solutionFolder = $PSScriptRoot + "..\..\"
$projectFile = $solutionFolder + "\DALTest\DAL.Test.csproj"
$options = "/p:Configuration=Release"

$VersionRegex = "\d+\.\d+\.\d+\.\d+"
$VersionData = [regex]::matches($Env:BUILD_BUILDNUMBER,$VersionRegex)

if ($VersionData.Count -eq 0) {
    Write-Host "VERSION NOT FOUND"
}
else {
    Write-Host "VERSION FOUND"
}

& $msbuild $projectFile $options

if ($LastExitCode -ne 0){
    $exitCode=$LastExitCode
    Write-Error 'Build failed!'
    exit $exitCode
}
else{
    Write-Host 'Build succeeded'
}