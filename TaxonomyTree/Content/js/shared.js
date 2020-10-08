var dataType = 'taxonomy.flat';
var myData = [];
var myDataET = [];
var filterWords = [];
var tmpFilterWords = [];
var ontologyCheckDone = false;

$(".menuItem").click(function ()
{
    if (!$(this).hasClass('selected'))
    {
        if ($(this).attr('displayType') == 'action.refresh.taxonomies')
        {
            if (confirm('Are you sure you want to refresh all taxonomies?'))
            {
                var token = prompt("Authorization token:", "");
                if (!(token == null || token == ""))
                {

                    $('.dataTable').html('');
                    $('.infoText').show();

                    $.post("/Home/DownloadJSONTaxonomies?token=" + token, function (response)
                    {
                        alert('Taxonomy/ontology terms downloaded: ' + response);
                        location.reload();
                    });
                }
            }
        }
        else
        {
            if ($(this).attr('displayType') == 'action.refresh.engage.terms')
            {
                if (confirm('Are you sure you want to refresh all engage terms?'))
                {
                    var token = prompt("Authorization token:", "");
                    if (!(token == null || token == "")) {

                        $('.dataTable').html('');
                        $('.infoText').show();

                        $.post("/Home/DownloadJSONEngageTerms?token=" + token, function (response) {
                            alert('Engage terms downloaded: ' + response);
                            location.reload();
                        });
                    }
                }
            }
            else
            {
                dataType = $(this).attr('displayType');

                $(".menuItem").removeClass('selected');
                $(this).addClass('selected')

                if (dataType == 'engage.terms') loadDataET()
                else showTable()
            }
        }
    }
});

function loadDataET()
{
    if (myDataET.length == 0)
    {
        $('.dataTable').html('');
        $('.infoText').show();

        $.post("/Home/GetJSONEngageTerms", function (response)
        {
            myDataET = response;
            showTableET()
        });
    }
    else showTableET()
}

function showTableET()
{
    $('.dataTable').html('');
    $('.infoText').show();

    html = '';
    html += '<table style="width:100%">';
    html += '<tr class="rowHeader"><td><b>Term</b></td><td><b>Selectable</b></td><td><b>Code</b></td><td><b>Path</b></td></tr>';
    n = 0;
    nr = 0;
    oldCate = ['', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''];
    $.each(myDataET, function (i, obj)
    {
        if (filterAppliesET(obj))
        {
            nr++;
            tree = this.magic_tree;
            tree = tree.split('//');

            for (nc = 0; nc <= tree.length - 2; nc++)
            {
                if (oldCate[nc] != tree[nc]) {
                    n++;
                    html += '<tr class="row' + (n % 2 == 0 ? 'Even' : 'Odd') + '">';
                    html += '<td colspan="4" style="padding-left:' + (5 + nc * 25) + 'px;"><b>' + tree[nc] + '</b></td>';
                    html += '</tr>';
                    oldCate[nc] = tree[nc]
                }
            }

            n++;
            html += '<tr class="row' + (n % 2 == 0 ? 'Even' : 'Odd') + '">';
            html += '<td style="padding-left:' + (15 + (tree.length - 2) * 25) + 'px;">' + this.label + '</td><td>' + (this.selectable ? 'Yes' : 'No') + '</td><td>' + this.class_code + '</td><td>' + this.magic_tree + '</td>';
            html += '</tr>';
        }
        $("#searchInfo").html(nr + ' results');
    });
    html += '</table>';

    $('.dataTable').html(html);
    $('.infoText').hide();
}

function showTable()
{
    $('.dataTable').html('');
    $('.infoText').show();

    html = '';
    html += '<table style="width:100%">';
    if ((dataType == 'taxonomy.flat') || (dataType == 'ontology.flat'))
    {
        html += '<tr class="rowHeader"><td style="width: 60px"><b>Id</b></td><td style="width: 150px"><b>Path</b></td><td style="width: 250px"><b>Taxonomy&nbsp;Term</b></td><td><b>Definition</b></td></tr>';
        n = 0;
        $.each(myData, function (i, obj)
        {
            if (!ontologyCheckDone)
            {
                if (!this.is_practice)
                {
                    $('#ofv').show();
                    $('#otv').show();
                    $('#dtt').show();
                    $('#det').show();
                    ontologyCheckDone = true;
                }
            }
            if (filterApplies(obj))
            {
                n++;
                if (this.definition == null) this.definition = '';
                html += '<tr class="row' + (n % 2 == 0 ? 'Even' : 'Odd') + ' ' + (!this.is_practice ? "rowItalic" : "") + '"><td>' + this.taxonomy_id + '</td><td>' + this.magic_tree + '</td><td>' + this.text + '</td><td>' + this.definition + '</td></tr>';
            }
        });
        $("#searchInfo").html(n + ' results');
    }
    if ((dataType == 'taxonomy.tree') || (dataType == 'ontology.tree'))
    {
        html += '<tr class="rowHeader"><td style="width: 60px"><b>Parent&nbsp;Id</b></td><td style="width: 60px"><b>Term&nbsp;Id</b></td><td style="width: 250px"><b>Taxonomy&nbsp;Term</b></td><td><b>Definition</b></td></tr>';
        n = 0;
        nr = 0;
        oldCate = ['', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''];
        $.each(myData, function (i, obj)
        {
            if (filterApplies(obj))
            {
                nr++;
                if (this.definition == null) this.definition = '';
                tree = this.magic_tree;
                tree = tree.split('//');
                
                for (nc = 0; nc <= tree.length - 2; nc++)
                {
                    if (oldCate[nc] != tree[nc])
                    {
                        n++;
                        html += '<tr class="row' + (n % 2 == 0 ? 'Even' : 'Odd') + '">';
                        html += '<td colspan="4" style="padding-left:' + (5 + nc * 25) + 'px;"><b>' + tree[nc] + '</b></td>';
                        html += '</tr>';
                        oldCate[nc] = tree[nc]
                    }
                }

                n++;
                html += '<tr class="row' + (n % 2 == 0 ? 'Even' : 'Odd') + ' ' + (!this.is_practice ? "rowItalic" : "") + '">';
                html += '<td colspan="4" style="padding-left:' + (15 + (tree.length - 2) * 25) + 'px;"><table><tr><td style="width:50px">' + this.parent_taxonomy_id + '</td><td style="width:50px">' + this.taxonomy_id + '</td><td style="width:200px">' + this.text + '</td><td>' + this.definition + '</td></tr></table></td>';
                html += '</tr>';
            }
            $("#searchInfo").html(nr + ' results');
        });
    }
    html += '</table>';

    $('.dataTable').html(html);
    $('.infoText').hide();
}

function filterApplies(obj)
{
    if ((dataType == 'taxonomy.flat') || (dataType == 'taxonomy.tree'))
    {
        if (obj.is_practice != 1) return false
    }

    /*
    var tmpFilterWords = [];
    $.each(filterWords, function (i, word) { tmpFilterWords.push(word) });
    if ($("#searchBox").val() != '')
    {
        txt = obj.text.toLowerCase() + ' ' + obj.definition.toLowerCase();
        lstWords = txt.split(' ');
        $.each(lstWords, function (i, word)
        {
            var i = tmpFilterWords.indexOf(word);
            if (i !== -1) tmpFilterWords.splice(i, 1);
        })
        return (tmpFilterWords.length == 0 ? true : false)
    }
    else return true
    */

    var wordsFound = 0;
    if ($("#searchBox").val() != '')
    {
        var txt = obj.text + ' ' + obj.definition;
        txt = txt.split(',').join(' ');
        txt = txt.split('.').join(' ');
        txt = txt.split(';').join(' ');
        txt = txt.split(':').join(' ');
        txt = txt.split('(').join(' ');
        txt = txt.split(')').join(' ');
        txt = txt.replace(/\s\s+/g, ' ');
        $.each(filterWords, function (i, word)
        {
            if (word.slice(-1) == '*') myReg = new RegExp("\\b(" + word.toLowerCase() + "\\w*)\\b", "i");
            else if (word.charAt(0) == '*') myReg = new RegExp("\\b(\\w" + word.toLowerCase() + ")\\b", "i");
            else myReg = new RegExp("\\b(?:^|\\W)(" + word + ")(?:$|\\W)\\b", "i") 
            myMatch = txt.match(myReg);
            if (myMatch != null) wordsFound++;
        })
        return (filterWords.length == wordsFound ? true : false)
    }
    else return true
}

function filterAppliesET(obj)
{
    var tmpFilterWords = [];
    $.each(filterWords, function (i, word) { tmpFilterWords.push(word) });
    if ($("#searchBox").val() != '')
    {
        txt = obj.label.toLowerCase();
        lstWords = txt.split(' ');
        $.each(lstWords, function (i, word)
        {
            var i = tmpFilterWords.indexOf(word);
            if (i !== -1) tmpFilterWords.splice(i, 1);
        })
        return (tmpFilterWords.length == 0 ? true : false)
    }
    else return true
}

$(document).ready(function ()
{
    $.post("/Home/GetJSONTaxonomies", function (response)
    {
        myData = response;
        showTable()
    });
});

$("#filterButton2").bind("click", function (e)
{
    $("#searchBox").val('');
    $("#searchInfo").html('');
    if (dataType == 'engage.terms') showTableET()
    else showTable()
});

$("#filterButton").bind("click", function (e)
{
    filterWords = [];
    $("#searchInfo").html('');
    aux = $("#searchBox").val().toLowerCase().split(' ');
    $.each(aux, function (i, word) {
        if (word != '') filterWords.push(word)
    })
    if (dataType == 'engage.terms') showTableET()
    else showTable()
});

function checkEnter(e)
{
    e = e || event;
    var txtArea = /textarea/i.test((e.target || e.srcElement).tagName);
    if (e.keyCode == 13) $("#filterButton").click();
    else
    {
        return txtArea || (e.keyCode || e.which || e.charCode || 0) !== 13;
    }
}

document.querySelector('input').onkeypress = checkEnter;