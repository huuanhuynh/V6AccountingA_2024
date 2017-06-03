@echo off
set serviceName="V6SyncServices"
set folder=%cd%
set action="%1"
if %action% == "install" goto install
if %action% == "uninstall" goto uninstall
if %action% == "start" goto start
if %action% == "stop" goto stop
goto exit

:install
@echo on
cls
sc stop %serviceName%
sc delete %serviceName%
sc create %serviceName% binPath= "%folder%\V6SyncServices.exe"
sc config %serviceName% start= auto
@echo off
goto exit

:uninstall
@echo on
sc stop %serviceName%
sc delete %serviceName%
@echo off
goto exit

:stop
@echo on
sc stop %serviceName%
@echo off
goto exit

:start
@echo on
sc start %serviceName%
@echo off
goto exit

:exit