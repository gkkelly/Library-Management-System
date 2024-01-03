using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Books;
using RestSharp;
using Newtonsoft.Json; 

namespace DadLibrarySystem
{

    // GoogleBooksResponse.cs
    public class GoogleBooksResponse
    {
        public List<Book> Items { get; set; }
    }

    // Book.cs
    public class Book
    {
        public VolumeInfo VolumeInfo { get; set; }
    }

    public class GoogleBooksService
    {
        private const string ApiKey = "AIzaSyBnCAdGvid3BE7IiJUPhk-NbF_lWqxzloU";
        private const string BaseUrl = "https://www.googleapis.com/books/v1/";

        private readonly RestClient _client;

        public GoogleBooksService()
        {
            _client = new RestClient(BaseUrl);
        }

        public List<Book> SearchBooks(string query, string searchType)
        {
            var request = new RestRequest("volumes", Method.Get);
            request.AddParameter(searchType, query);
            request.AddParameter("key", ApiKey);

            var response = _client.Execute<GoogleBooksResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data?.Items;
            }

            return null;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AccessInfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public bool embeddable { get; set; }
        public bool publicDomain { get; set; }
        public string textToSpeechPermission { get; set; }
        public Epub epub { get; set; }
        public Pdf pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
        public bool quoteSharingAllowed { get; set; }
    }

    public class Epub
    {
        public bool isAvailable { get; set; }
    }

    public class ImageLinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }

    public class IndustryIdentifier  //ISBN identified
    {
        public string type { get; set; }  // ISBN10 or 13
        public string identifier { get; set; } // ISBN
    }

    public class Item
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public VolumeInfo volumeInfo { get; set; }
        public SaleInfo saleInfo { get; set; }
        public AccessInfo accessInfo { get; set; }
        public SearchInfo searchInfo { get; set; }
    }

    public class PanelizationSummary
    {
        public bool containsEpubBubbles { get; set; }
        public bool containsImageBubbles { get; set; }
    }

    public class Pdf
    {
        public bool isAvailable { get; set; }
    }

    public class ReadingModes
    {
        public bool text { get; set; }
        public bool image { get; set; }
    }

    //public class Root
    //{
    //    public string kind { get; set; }
    //    public int totalItems { get; set; }
    //    public List<Item> items { get; set; }
    //}

    public class SaleInfo
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }
    }

    public class SearchInfo
    {
        public string textSnippet { get; set; }
    }

    public class VolumeInfo
    {
        public string title { get; set; }  // title  
        public string subtitle { get; set; }
        public List<string> authors { get; set; }  // authors
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public List<IndustryIdentifier> industryIdentifiers { get; set; }
        public ReadingModes readingModes { get; set; }
        public int pageCount { get; set; }
        public string printType { get; set; }
        public List<string> categories { get; set; }  // fiction vs nonfiction
        public string maturityRating { get; set; }
        public bool allowAnonLogging { get; set; }
        public string contentVersion { get; set; }
        public PanelizationSummary panelizationSummary { get; set; }
        public ImageLinks imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
    }


}
