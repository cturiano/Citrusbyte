function buildReadingsTable() {

   var parts = document.URL.split("/");
   var id = parts[parts.length - 1];

   if (!($.isNumeric(id))) {
      var deviceId = document.getElementById("deviceId");
      if (deviceId) {
         id = deviceId.className;
      } else {
         id = null;
      }
   }

   var start = null;
   var startElement = document.getElementById("startDate");
   if (startElement) {
      start = startElement.value;
      if (start) {
         start += " 12:00:00 AM";
      }
   }
               
   var end = null;
   var endElement = document.getElementById("endDate");
   if (endElement) {
      end = endElement.value;
      if (end) {
         end += " 11:59:59 PM";
      }
   }

   $.ajax({
      url: "/SensorReadings/BuildReadingsTable",
      data: { "deviceId": id, "start": start, "end": end },
      success: function(result) {
         $("#tableDiv").html(result); 
         startElement.value = "";
         endElement.value = "";
      }
   }); 
}