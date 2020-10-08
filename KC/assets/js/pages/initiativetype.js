var back_loc = '/Home/ShowData?data=initypes';

$("#formLoading").hide();
$("#mainForm").show();

$('#hb_link').attr('href', back_loc);

$("#btnSubmit").click(function ()
{
	$('#description').val(mailBodyEditor.root.innerHTML);
	$("#mainForm").submit();
});

$("#btnCancel").click(function () {
	location = back_loc;
});

function startPage()
{
	//body editor
	mailBodyEditor = new Quill('#kt_description', {
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