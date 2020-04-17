
var beneficiariosAlt = [];
$(document).ready(function () {
    beneficiariosAlt = [];
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #CPF').val(obj.CPF);
        beneficiariosAlt = obj.Beneficiarios;
    }
    $("#CPF").mask('000.000.000-00');
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": $(this).find("#CPF").val(),
                "Beneficiarios": beneficiariosAlt
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();                                
                window.location.href = urlRetorno;
            }
        });
    })


    $('#btn-beneficiarios').click(function (e) {
        e.preventDefault();


        $.post("../Beneficiarios", "").done(function (res) {
            $('body').append(res);

            $('#benefi').modal('show');

            $('#btn-incluir-beneficiario').click(function (e) {
                e.preventDefault();
                beneficiariosAlt.push({ "CPF": $("#BeCPF").val(), "Nome": $("#BeNome").val() })
                printGridBeneficiarios();
                $("#BeCPF").val("");
                $("#BeNome").val("");
            });

            $("#BeCPF").mask('000.000.000-00');
            printGridBeneficiarios();
        });
    });


})

function printGridBeneficiarios() {

    $("#gridBeneficiarios tbody").empty();
    beneficiariosAlt.forEach(function (item, index) {
        $("#gridBeneficiarios tbody").append("<tr><td>" + item.CPF + "</td><td>" + item.Nome + "</td><td> <button type='button' onclick='editarBeneficiarios(\"" + item.CPF + "\", \"" + item.Nome + "\")'  class='btn btn-primary btn-sm'>Editar</button> <button type='button' onclick='deleteBeneficiarios(\"" + item.CPF + "\", \"" + item.Nome + "\")'  class='btn btn-primary btn-sm'>Remover</button></td><tr>");
    });
}

function deleteBeneficiarios(cpf, nome) {
    beneficiariosAlt = beneficiariosAlt.filter(function (value, index, arr) {
        return (value.CPF != cpf || value.Nome != nome);
    });
    printGridBeneficiarios();
}

function editarBeneficiarios(cpf, nome) {
    $("#BeCPF").val(cpf);
    $("#BeNome").val(nome);
    deleteBeneficiarios(cpf, nome);
}


function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
