var back_loc = '/Home/ShowData?data=roles';

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

function startPage()
{
//	if (iniType == 'KI')
	{
		$('#iniBadge').addClass('badge-danger');
		$('#iniTitle').html('Knowledge Investment');
	}
//	if (iniType == 'PK')
	{
	//	$('#iniBadge').addClass('badge-warning');
	//	$('#iniTitle').html('Asset Investment (large proprietary knowledge investment)');
	}
}