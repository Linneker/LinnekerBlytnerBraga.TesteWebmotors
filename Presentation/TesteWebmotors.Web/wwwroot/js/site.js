// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function buscaModeloPelaMarca(nomeIdMarcas) {
    var makeId = $("#" + nomeIdMarcas).val();
    if (makeId == undefined || makeId == 0 || makeId == '') {
        $("#versao").empty();
        return;
    }
    $.ajax({
        url: "/Model/ObterModelPeloMakeId?makeId=" + makeId,
        success: function (data) {
            $("#models").empty();
            $("#versao").empty();
            $("#models").append('<option value>Selecione...</option>');
            $.each(data, function (index, element) {
                $("#models").append('<option value="' + element.ID + '">' + element.name + '</option>');
            });
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function buscaVersaoPelaMarca() {
    var modelId = $("#models").val();
    if (modelId == undefined || modelId == 0 || modelId == '') {
        alert("Identificador Invalido!");
        return;
    }
    $.ajax({
        url: "/Version/ObterVersionPeloMakeId?modeloId=" + modelId,
        success: function (data) {
            $("#versao").empty();
            $("#versao").append('<option value>Selecione...</option>');
            $.each(data, function (index, element) {
                $("#versao").append('<option value="' + element.name + '">' + element.name + '</option>');
            });
        },
        error: function (e) {
            console.log(e);
        }
    });
}