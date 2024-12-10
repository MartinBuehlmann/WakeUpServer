@echo off
echo Are you sure to stop the WakeUp server?
echo (Press Ctrl-C to abort)
Pause

set raspi_wakeup_name=%raspi_wakeup_name%
set key_file="C:\temp\Raspberry\ssh_keys\id_rsa"

ssh -i "C:\temp\Raspberry\ssh_keys\id_rsa" pi@%raspi_wakeup_name% "sudo shutdown -h now"