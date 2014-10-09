using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Couchbase.Lite;

namespace HelloCBL.Android
{
    [Activity(Label = "HelloCBL.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Create a shared manager
            var manager = Manager.SharedInstance;
            System.Diagnostics.Debug.Assert(manager != null);
            Console.WriteLine("Manager created");

            var dbName = "hello";
            System.Diagnostics.Debug.Assert(Manager.IsValidDatabaseName(dbName));

            // Create a document
            var database = manager.GetDatabase(dbName);
            System.Diagnostics.Debug.Assert(database != null);
            Console.WriteLine("Database created");

            var properties = new Dictionary<string, object>()
            {
                {"message", "Hello Couchbase Lite"},
                {"created_at", DateTime.UtcNow.ToString("o")},
            };

            // Create a document
            var document = database.CreateDocument();
            System.Diagnostics.Debug.Assert(document != null);

            var revision = document.PutProperties(properties);
            System.Diagnostics.Debug.Assert(revision != null);

            var docId = document.Id;
            Console.WriteLine("Document created with ID = {0}", docId);

            // Retrieve a document
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
            System.Diagnostics.Debug.Assert(updatedRevision != null);

            Console.WriteLine("Updated document: ");
            foreach (var kvp in updatedRevision.Document.Properties)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
            }

            // Delete a document
            retrievedDocument.Delete();
            Console.WriteLine("Deleted document, deletion status: {0}", retrievedDocument.Deleted);
        }
    }
}


