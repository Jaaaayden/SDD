# Software Design & Development Capstone Project 2024-2025
An interactive VR game designed to help incoming CAMS students discover their interests in Biotechnology, Engineering, or Computer Science through immersive experiences.

'''

# Setup Instructions

1. Clone the repository
Clone this GitHub repository to your local machine:

    git clone https://github.com/Jaaaayden/SDD
    cd SDD

'''

2. Open in Unity
Open Unity Hub and add the cloned folder as a new project. Make sure you're using:

    Unity version: 2021.3.39f1

If you don’t have this version installed, you can download it via Unity Hub → Installs → Add.

'''

3. Install Android Build Support
In Unity Hub:
- Go to 'Installs'
- Click the three dots next to Unity 2021.3.39f1
- Choose 'Add Modules'
- Select:
    - Android Build Support
    - Android SDK & NDK Tools
    - OpenJDK
- Install

'''

4. Connect Your VR Headset
- Enable Developer Mode on your headset (e.g., via the Meta Horizons mobile app)
- Connect your headset to your PC with a USB-C cable
- Allow USB debugging if prompted in the headset

'''

5. Configure Build Settings
- In Unity: File → Build Settings
- Switch platform to **Android**
- Click "Build and Run" while the headset is plugged in

Unity will compile and deploy the app. After completion, the application will launch on the headset and can be reopened anytime from the apps menu.

'''

# Troubleshooting

• Headset not detected?
    → Make sure USB debugging (unplug and replug to get notification again) is allowed and the headset is in Developer Mode

• Build fail?
    → Double-check that Android modules are installed correctly via Unity Hub

• Android API Level error?
    → Go to: Edit → Project Settings → Player → Android
       - Set Minimum API Level to Android 6.0 'Marshmallow' (API level 23) 
       - Set Target API Level to Automatic (highest installed)
       → Make sure matching SDKs are installed under Android Build Support

Direct any issues to jaydenle360@gmail.com

'''

# Credits
Special thanks to:

### [Josh Pham](https://github.com/Nqchoz)
- Engineering pathway developer
### [Justin Sato](https://github.com/LittleHalf)
- Biotechnology pathway developer
### [Angel Ortega](https://github.com/AngelOrtega06) and [Angelica Benitez](https://github.com/angelicabz8)
### Abraham Kim and Alana Alonso
- Graphic/sound design
### Justin Sipraseuth and Ashley Portillo
- Project managers
### August Montgomery, Megan Lee, and Noemi Alonso
- Marketing
