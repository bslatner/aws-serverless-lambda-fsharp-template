@echo off
cls


.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

"packages\FAKE.Core\tools\Fake.exe" build.fsx
