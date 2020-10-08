var dt = '';
var df = '';
var controller = '';

function startPage()
{
	$("#formLoading").hide();
	$("#mainRegion").show();

	dt = getUrlParameter('data');
	df = getUrlParameter('filter');

	setControler();

	if (dt == 'initypes') showInitiativeTypes();
	if (dt == 'clientinv') showClientInvolvements();
	if (dt == 'external3p') showExternalThirdParties();
	if (dt == "perdiems") showPerDiems();
	if (dt == "staffing") showStaffing();
	if (dt == "stages") showProjectStages();
	if (dt == "roles") showTeamRoles();
	if (dt == "endproducts") showEndProducts();
	if (dt == "clienteng") showClientEngagements();
	if (dt == "approvers") showApprovers();
	if (dt == "notifirec") showNotificationsRecipients();
	if (dt == "notifitri") showNotificationsTriggers();
	if (dt == "notifications") showNotifications();
	if (dt == "departments") showDepartments();
	if (dt == "owners") showOwners();
}

function showInitiativeTypes()
{
	$('#shd_title').html('Initiative Type');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Code</th><th>Name</th><th style="width:120px">Allow creation</th><th style="width:50px"></th></tr></thead>')
	
	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key='+dt,
			columns: [				
				{ data: 'initiative_type_id' },
				{ data: 'code' },
				{ data: 'name' },
				{ data: 'allow_creation' },
				{ data: 'initiative_type_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.initiative_type_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.initiative_type_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.initiative_type_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.department_id + '" class="tableLink">' + (data ? 'Yes' : 'No') + '</a>' } },
					{ targets: [4], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.initiative_type_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showClientInvolvements() {
	$('#shd_title').html('Client Involvement');
	$('#shd_href').attr('href', '/' + controller +'/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Order</th><th>Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'client_involvement_id' },
				{ data: 'display_order' },
				{ data: 'name' },
				{ data: 'client_involvement_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.client_involvement_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.client_involvement_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.client_involvement_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.client_involvement_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showExternalThirdParties() {
	$('#shd_title').html('External Third Parties');
	$('#shd_href').attr('href', '/' + controller +'/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Order</th><th>Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'external_3rd_party_id' },
				{ data: 'display_order' },
				{ data: 'name' },
				{ data: 'external_3rd_party_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.external_3rd_party_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.external_3rd_party_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.external_3rd_party_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller +'/Edit?id=' + row.external_3rd_party_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showPerDiems() {
	$('#shd_title').html('Per Diem Rates');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:120px">Role</th><th>Value</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'perdiem_id' },
				{ data: 'role' },
				{ data: 'value' },
				{ data: 'perdiem_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.perdiem_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.perdiem_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.perdiem_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.perdiem_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showStaffing() {
	$('#shd_title').html('Staffing Types');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Order</th><th style="width:160px">Name</th><th>Description</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'staffing_id' },
				{ data: 'display_order' },
				{ data: 'name' },
				{ data: 'description' },
				{ data: 'staffing_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.staffing_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.staffing_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.staffing_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.staffing_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [4], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.staffing_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showProjectStages() {
	$('#shd_title').html('Project Stages');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Order</th><th style="width:160px">Name</th><th>Description</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'project_stage_id' },
				{ data: 'display_order' },
				{ data: 'name' },
				{ data: 'description' },
				{ data: 'project_stage_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.project_stage_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.project_stage_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.project_stage_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.project_stage_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [4], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.project_stage_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showTeamRoles() {
	$('#shd_title').html('Team Roles');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Order</th><th style="width:260px">Name</th><th>Legacy</th><th>Multiple names</th><th>Mandatory</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'team_role_id' },
				{ data: 'display_order' },
				{ data: 'name' },
				{ data: 'legacy' },
				{ data: 'can_multiple' },
				{ data: 'mandatory' },
				{ data: 'team_role_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.team_role_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.team_role_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.team_role_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.team_role_id + '" class="tableLink">' + (data?'Yes':'No') + '</a>' } },
					{ targets: [4], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.team_role_id + '" class="tableLink">' + (data ? 'Yes' : 'No') + '</a>' } },
					{ targets: [5], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.team_role_id + '" class="tableLink">' + (data ? 'Yes' : 'No') + '</a>' } },
					{ targets: [6], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.team_role_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showEndProducts() {
	$('#shd_title').html('Project End Products');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Order</th><th>Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'end_product_id' },
				{ data: 'display_order' },
				{ data: 'name' },
				{ data: 'end_product_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.end_product_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.end_product_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.end_product_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.end_product_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showClientEngagements() {
	$('#shd_title').html('Client Engagements');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Order</th><th>Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'client_engagement_id' },
				{ data: 'display_order' },
				{ data: 'name' },
				{ data: 'client_engagement_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.client_engagement_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.client_engagement_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.client_engagement_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.client_engagement_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showApprovers() {
	$('#shd_title').html('Approvers');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:160px">Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'approver_id' },
				{ data: 'name' },
				//{ data: 'proxy_approvers' },
				{ data: 'approver_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.approver_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.approver_id + '" class="tableLink">' + data + '</a>' } },
					//{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.approver_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.approver_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showNotificationsRecipients() {
	$('#shd_title').html('Notification Recipients');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:240px">Name</th><th>Show also in Reply To</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'notification_recipient_id' },
				{ data: 'name' },
				{ data: 'show_also_in_replyto' },
				{ data: 'notification_recipient_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_recipient_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_recipient_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_recipient_id + '" class="tableLink">' + (data ? 'Yes' : 'No') + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_recipient_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showNotificationsTriggers() {
	$('#shd_title').html('Notification Triggers');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th>Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'notification_trigger_id' },
				{ data: 'name' },
				{ data: 'notification_trigger_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_trigger_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_trigger_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_trigger_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	
function showNotifications() {
	$('#shd_title').html('Notifications');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th>Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'notification_id' },
				{ data: 'name' },
				{ data: 'notification_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.notification_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};
function showDepartments() {
	$('#shd_title').html('Departments');
	$('#shd_href').hide();
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th style="width:60px">Code</th><th>Name</th><th style="width:160px">Allow Initiative Creation</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'department_id' },
				{ data: 'code' },
				{ data: 'name' },
				{ data: 'allow_initiative_creation' },
				{ data: 'team_role_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.department_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.department_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.department_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [3], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.department_id + '" class="tableLink">' + (data ? 'Yes' : 'No') + '</a>' } },
					{ targets: [4], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.department_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a>' } }
				]
		});
};	
function showOwners() {
	$('#shd_title').html('Owners');
	$('#shd_href').attr('href', '/' + controller + '/Edit?id=0');
	var table = $('#dataTable');
	table.html('<thead><tr><th style="width:40px">Id</th><th>Name</th><th style="width:50px"></th></tr></thead>')

	table.DataTable(
		{
			responsive: true,
			ajax: '/Home/GetJSONData?key=' + dt,
			columns: [
				{ data: 'owner_id' },
				{ data: 'name' },
				//{ data: 'owner_contacts' },
				{ data: 'owner_id' }
			],
			order: [[0, 'asc']],
			pageLength: 25,
			columnDefs:
				[
					{ targets: [0], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.owner_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [1], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.owner_id + '" class="tableLink">' + data + '</a>' } },
					//{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.approver_id + '" class="tableLink">' + data + '</a>' } },
					{ targets: [2], render: function (data, type, row) { return '<a href="/' + controller + '/Edit?id=' + row.owner_id + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit"><i class="flaticon-edit-1"></i></i></a><div onclick="deleteDocument(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Delete"><i class="flaticon-delete"></i></div>' } }
				]
		});
};	

function deleteDocument(id)
{
	if (confirm('Are you sure you want to delete this entry?'))
	{
		$.ajax({
			type: 'GET',
			url: '/' + controller+'/Delete?id=' + id,
			success: function (data)
			{
				if (data!='') alert(data);
				location.reload();
			}
		});
	}
}

function setControler()
{
	if (dt == 'initypes') controller = 'InitiativeType';
	if (dt == 'clientinv') controller = 'ClientInvolvement';
	if (dt == 'external3p') controller = 'ExternalThirdParty';
	if (dt == "perdiems") controller = 'PerDiemRate';
	if (dt == "staffing") controller = 'StaffingType';
	if (dt == "stages") controller = 'ProjectStage';
	if (dt == "roles") controller = 'TeamRole';
	if (dt == "endproducts") controller = 'EndProduct';
	if (dt == "clienteng") controller = 'ClientEngagement';
	if (dt == "approvers") controller = 'Approver';
	if (dt == "notifirec") controller = 'NotificationRecipient';
	if (dt == "notifitri") controller = 'NotificationTrigger';
	if (dt == "notifications") controller = 'Notification';
	if (dt == "departments") controller = 'Department';
	if (dt == "owners") controller = 'Owner';
}