function searchGuid() {
   var guid = null;
   var searchElement = document.getElementById("searchString");
   if (searchElement) {
      guid = searchElement.value;
   }

   if (guid) {
      $.ajax({
         url: "/api/WebApiDevices/GetDevice?serial=" + guid,
         type: "GET",
         success: function(result) {
            window.location.replace("/Devices/DetailsGuid/" + guid);
         },
         error: function(error) {
            searchElement.value = error.statusText;
         }
      });
   }
}