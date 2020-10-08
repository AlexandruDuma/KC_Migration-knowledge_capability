var back_loc = '/Home/ShowData?data=clientinv';

$("#formLoading").hide();
$("#mainForm").show();

$('#hb_link').attr('href', back_loc);

$("#btnSubmit").click(function ()
{
	$("#mainForm").submit();
});

$("#btnCancel").click(function () {
	location = back_loc;
});