# Couchbase Lite .NET tutorial app

This repo contains the iOS and Android HellCBL application used in the [tutorial for Couchbase Lite .NET](http://developer.couchbase.com/mobile/develop/training/build-first-net-app/index.html). The tutorial shows how to bring the Couchbase Lite NuGet package into your app, create a database, and do basic CRUD operations. To focus on using Couchbase Lite, the app is highly simplified and provides only console output in the simulator. (There's no UI output other than a plain white background screen).

## Downloading and running the tutorial app

1. Change to the directory in which you want to place the code:

	$ cd ~/repos

2. Clone the repository:

	$ git clone https://github.com/couchbaselabs/couchbase-lite-tutorial-net.git

3. Change to the directory that contains the repo:

	$ cd couchbase-lite-tutorial-net

4. In Xamarin Studio, open the solution file found at **/couchbase-lite-tutorial-net/HelloCBL/HelloCBL.sln**.

5. To run HelloCBL.iOS, right click at the HelloCBL.iOS project and choose **'Set As Startup Project'** from the menu.

6. Choose your iPhone simulator or device as desire and click 'Run' button. You can view the output in the Application Output console.

7. To run HelloCBL.Android, right click at the HelloCBL.Android project and choose **'Set As Startup Project'** from the menu.

6. Configure or Select your Android simulator or device and click 'Run' button. You can view the output in the Application Output console.
