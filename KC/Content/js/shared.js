var myData = [];

$(".menuItem").click(function ()
{
    if (!$(this).hasClass('selected'))
    {
        dataType = $(this).attr('displayType');

        $(".menuItem").removeClass('selected');
        $(this).addClass('selected')

        if (dataType == 'config.initiative.types') location = '/Configuration/ShowInitiativeTypes'
        if (dataType == 'config.project.stages') location = '/Configuration/ShowProjectStages'
    }
});


$(document).ready(function ()
{

});



function checkEnter(e)
{
    e = e || event;
    var txtArea = /textarea/i.test((e.target || e.srcElement).tagName);
    return txtArea || (e.keyCode || e.which || e.charCode || 0) !== 13;
}

document.querySelector('input').onkeypress = checkEnter;