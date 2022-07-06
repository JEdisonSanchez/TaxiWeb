function loginUser() {
    $.ajax(
        {
            "url": "http://localhost:54922/Home/Login",
            "type": "POST",
            "data": {
                'usuario': $('#usuario').val(),
                'password': $('#password').val()
            },
            "dataType": "json",                              //tipo archivo
            "success": function (dataServer) {
                //funcion que se debe ejecutar desde javascript
                if (dataServer.error) {
                    alert(dataServer.error);
                } else {
                    window.location.href = 'http://localhost:54922/Home/Index';
                    //$('.bienvenido h1').html('Bienvenido' + dataServer.usuario)
                }

            },
            "error": function () {
                //hacer algo
                alert("Error!!");
            }
        }
    );
}