
function ShowAlert(msg,second)
{
	//if(type==1)
	//	$("#divAlert").addClass("am-alert-success");
	//else if (type == 2)
	//	$("#divAlert").addClass("am-alert-warning");
	//else if (type == 3)
	//	$("#divAlert").addClass("am-alert-danger");

	//$("#divAlert p").html(msg);
	//$("#divAlert").alert();

	$("#my-alert .am-modal-hd").html(msg);
	$('#my-alert').modal();
	window.setTimeout(CloseAlert, second * 1000);
}

function CloseAlert()
{
	//$("#divAlert").alert("close");
	$('#my-alert').modal("close");
}