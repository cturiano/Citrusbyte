$(function() {
   $(".datePicker").datepicker({
      changeMonth: true,
      changeYear: true,
      yearRange: "-100:+0",
      controlType: "select",
      dateFormat: "mm/dd/yy"
   });
});