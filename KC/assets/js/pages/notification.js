var back_loc = '/Home/ShowData?data=notifications';

$("#formLoading").hide();
$("#mainForm").show();

$('#hb_link').attr('href', back_loc);

$("#btnSubmit").click(function ()
{
	$('#body').val(mailBodyEditor.root.innerHTML);
	$("#mainForm").submit();
});

$("#btnCancel").click(function () {
	location = back_loc;
});

function startPage()
{
	//select2 - also_send_to list
	$('#also_send_to').select2(
		{
			ajax: {
				url: '/Home/GetActiveDirectoryPersons?key=',
				dataType: 'json',
				processResults: function (data) { return unescapeData(data) }
			}
		});
	//add values
	$.each(js_astList.split(";"), function (i, e) {
		if (e != '') {
			var option = new Option(e, e, true, true);
			$("#also_send_to").append(option).trigger('change');
		}
	});
	//refresh select2 control
	$("#also_send_to").trigger('change');


	//select2 - also_reply_to list
	$('#also_reply_to').select2(
		{
			ajax: {
				url: '/Home/GetActiveDirectoryPersons?key=',
				dataType: 'json',
				processResults: function (data) { return unescapeData(data) }
			},
			tags: true
		});
	//add values
	$.each(js_artList.split(";"), function (i, e) {
		if (e != '') {
			var option = new Option(e, e, true, true);
			$("#also_reply_to").append(option).trigger('change');
		}
	});
	//refresh select2 control
	$("#also_reply_to").trigger('change');

	//body editor
	mailBodyEditor = new Quill('#kt_mail_body', {
		modules: {
			toolbar: [
				[{ header: [] }],
				['bold', 'italic', 'underline', 'link'],
				[{ color: [] }, { background: [] }],
				[{ list: 'ordered' }, { list: 'bullet' }],
				['clean']
			]
		},
		placeholder: 'Type your text here...',
		theme: 'snow'
	});					
}