# MixerFixer
A Mixer app for Windows to better control audio levels and overriding Windows depending on how it’s configured.

### Youtube Demo
[![Youtube Demo](https://img.youtube.com/vi/rUBA_o1_Mrg/hqdefault.jpg)](https://www.youtube.com/watch?v=rUBA_o1_Mrg)


## Info about the app
The app is built using WPF as our base, but 99% of the app and its functionality is done as a web app and the WPF part only acts as a host for the Web App. Using Microsoft’s new-ish WebView2 browser control we can browse to the app’s embedded webpage which is also hosted inside of the WPF app using Kestrel (web server).

This means we can also access the app from a local browser or even over the local network. The app also uses SignalR (web sockets) for a very responsive UI with live updates to the UI as changes are detected.

## What can it do
1. Control Master, Application/Game, and Microphone Volume
2. Able to store volume levels of all Applications in local settings file and restore volume levels automatically.
3. Set a default Application volume which the app will use to set the volume level of any application/game starting up.
4. Easy way to set mute of Master or Application audio by just using Right Click on a volume control.
5. Set device priority. MixerFixer can set the Default Audio device automatically when devices become active or inactive based on the priority set by the user.
6. Basic management of Displays. Store and restore the state of a Display. Enable or disable a Display.
7. Easily change colour scheme live using RGB sliders.
8. Can access controls over a local network using a browser with the built in QR Code (Other PC, tablet, phone, anything with a browser)
9. Tray Icon. Run without a UI.
10. Auto startup with Windows.

## Control Volume
Use the sliders to control the volume level of the current Default Device and any applications that may be producing sound.

With mouse cursor over slider, hold left CTRL and use scroll wheel on mouse to change volume.
![image](https://user-images.githubusercontent.com/13113785/215668162-4d1b96b5-caab-43f2-8043-7bce24d43468.png)


Right Click anywhere on volume control to switch Mute state of the device or application.
![image](https://user-images.githubusercontent.com/13113785/215668032-5923fa8d-a79a-40db-ba32-898ade14529c.png)


Left click Is Managed switch which will make MixerFixer keep the current volume of the device or application. While MF is running and a device or app is marked as Is Managed, the volume or mute state cannot be changed in Windows. Can only change in MF when using Is Managed.
![image](https://user-images.githubusercontent.com/13113785/215668659-f2e2c98a-adbf-4451-b811-0a3d99bae701.png)

Under Settings Window you can enable ‘Use Default Volume’ which will apply a default volume level to any app that starts up. If the app is marked as Is Managed, then this default will not apply. Click on the button to change what the default app volume should be.
![image](https://user-images.githubusercontent.com/13113785/215672396-0de572be-0224-46b5-9fba-56c7d8316686.png)


## Device priority
When switching devices, like plugging in a USB headset, MixerFixer can ensure the correct device is made the Default based on a user configurable priority. Priority can be set for sound output as well as for sound capture (Mic).
To set up, open the settings window.
![image](https://user-images.githubusercontent.com/13113785/215669530-d641c424-9671-4694-b894-63787c295213.png)

Then move the device in required priority and then toggle the switch on the devices you want MF to manage for you.
![image](https://user-images.githubusercontent.com/13113785/215669717-c46440d8-9d92-42dd-b09f-5d1b15dd190c.png)

## Display Settings
(Extra functionality I wanted)
MixerFixer can store the state of your Displays and then restore that state at a later stage. The idea is to mark in MF which displays you want to be managed at which point the state of the selected Display(s) will be stored. When a display is enabled at a later stage, MF will automatically restore that Display’s state ensuring it’s resolution, position, frequency, ect. is back to how it was when MF stored it.

I use it on my 4th Monitor which I don’t use all the time as Windows 10 will reset it’s position every time I disable it because this monitor’s resolution is smaller than my other 3. If the 4th monitor had the same resolution then it looks like Win10 will remember the position correctly, but with Windows you don’t really ever have any guarantees.

First, open the Display Settings Window:

![image](https://user-images.githubusercontent.com/13113785/215671386-41d87f47-9506-45df-9c9c-ebff610fca4d.png)

Then mark which Display you wish to have MixerFixer manage. Can also toggle to enable or disable a Display on this screen.
![image](https://user-images.githubusercontent.com/13113785/215671656-1a4802c8-bf13-45bb-ac0d-3c2a29cf84eb.png)


## Theming
Using the Theme window you can easily change the colours of the app and see the changes live.

First open the Theme Window.

![image](https://user-images.githubusercontent.com/13113785/215672642-eaa5ff31-169a-468f-8b08-322098be8b8c.png)

Then you can drag the sliders to change the red, green, and blue values for each of the 3 main app colours.
![image](https://user-images.githubusercontent.com/13113785/215672812-75a59503-15c8-4946-a8f6-99788d96df7d.png)


## Access using a browser.
You can open MixerFixer using a browser. This can be done using a browser on the machine the app is running on or even over the network using for example your phone.

First open the Remote Access Window.

![image](https://user-images.githubusercontent.com/13113785/215673253-24c7c9c4-2051-435e-9868-1de49576dfd4.png)

Then you can click the button to open in your local default browser or scan the QR Code if using your phone.
![image](https://user-images.githubusercontent.com/13113785/215676076-182a7962-9cbf-46c2-b583-ce1a14078a1a.png)


## Run minimised & Auto Startup
MixerFixer can run without the main window being open. It will always have a Tray Icon. Left clicking the Tray Icon will open the Main Window and right click on the Tray Icon will give hte open to exit the app completely.

![image](https://user-images.githubusercontent.com/13113785/215676518-e0f90dbe-4127-4d42-b2ae-d4e0eb4c43a8.png)

Under the Settings Window you can enable the toggle switch to make MixerFixer startup without the main window showing.
![image](https://user-images.githubusercontent.com/13113785/215676743-10b53e00-2f03-43a7-9c6b-0e5ec32c91aa.png)

Also under the Settings Window you can set if MixerFixer should start with Windows.
![image](https://user-images.githubusercontent.com/13113785/215676933-cc0110e8-3ecc-42cd-ad58-bf64496f1892.png)
