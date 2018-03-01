@echo off
cls

if not exist .paket\paket.exe (
  .paket\paket.bootstrapper.exe
)

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

"packages\FAKE\tools\Fake.exe" build.fsx
