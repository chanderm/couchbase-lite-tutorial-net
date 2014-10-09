using System;
using System.Collections.Generic;
using System.Linq;
using Couchbase.Lite;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;

namespace HelloCBL.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            window = new UIWindow(UIScreen.MainScreen.Bounds);
			
            // If you have defined a root view controller, set it here:
            window.RootViewController = new UIViewController();
			
            // make the window visible
            window.MakeKeyAndVisible();
			
            // Create a shared manager
            var manager = Manager.SharedInstance;
            Debug.Assert(manager != null);
            Console.WriteLine("Manager created");

            // Create a database
            var dbName = "hello";
            Debug.Assert(Manager.IsValidDatabaseName(dbName));

            var database = manager.GetDatabase(dbName);
            Debug.Assert(database != null);
            Console.WriteLine("Database created");

            // Create a document
            var properties = new Dictionary<string, object>()
            {
                {"message", "Hello Couchbase Lite"},
                {"created_at", DateTime.UtcNow.ToString("o")},
            };

            var document = database.CreateDocument();
            Debug.Assert(document != null);

            var revision = document.PutProperties(properties);
            Debug.Assert(revision != null);

            var docId = document.Id;
            Console.WriteLine("Document created with ID: {0}", docId);

            // Retrieve the document from the database
            var retrievedDocument = database.GetDocument(docId);

            Console.WriteLine("Retrieved document: ");
            foreach (var kvp in retrievedDocument.Properties)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
            }

            // Update a document
            var updatedProperties = new Dictionary<string, object>(retrievedDocument.Properties);
            updatedProperties["message"] = "We're having a heat wave!";
            updatedProperties["temperature"] = 95.0;

            var updatedRevision = retrievedDocument.PutProperties(updatedProperties);
            Debug.Assert(updatedRevision != null);

            Console.WriteLine("Updated document: ");
            foreach (var kvp in updatedRevision.Document.Properties)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
            }

            // Delete a document
            retrievedDocument.Delete();
            Console.WriteLine("Deleted document, deletion status: {0}", retrievedDocument.Deleted);

            return true;
        }
    }
}

