@ECHO OFF
set address=wakeup.erowa.global
set password=pocketpc2002

plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/WakeUpServer"
plink -ssh pi@%address% -pw %password% -no-antispoof "mkdir /home/pi/WakeUpServer/bin"
scp -r WakeUpServer.service pi@%address%:/lib/systemd/system/
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemd daemon-reload"
plink -ssh pi@%address% -pw %password% -no-antispoof "sudo systemctl enable WakeUpServer"
