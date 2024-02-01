@ECHO OFF
set address=wakeup.erowa.global
set password=pocketpc2002
cd ..
cd source
rd ..\artifacts /s /q

REM Angular
cd frontend
call npm run build
cd ..

REM .NET
cd backend
dotnet publish WakeUpServer -c Release -r linux-arm --self-contained -o ../../artifacts/publish/raspberry /p:DebugType=None /p:DebugSymbols=false

REM Publish
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl stop WakeUpServer"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/WakeUpServer"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/WakeUpServer/bin"
pscp -pw %password% -r ../../artifacts/publish/raspberry/* pi@%address%:/home/pi/WakeUpServer/bin
plink -ssh pi@%address% -pw %password% -no-antispoof "chmod +x /home/pi/WakeUpServer/bin/WakeUpServer"
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl start WakeUpServer"
pause