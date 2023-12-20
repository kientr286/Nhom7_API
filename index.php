<?php
//index.php - Routing = Người chỉ đường
//Người lập trình = Người viết kịch bản

//Giả sử: Mọi URL có khuôn dạng
//http://localhost/phpmvc/index.php?c=x&a=y
//c- controller a - action (hàm cần gọi trong controller)

//http://localhost/phpmvc/index.php?c=student&a=all
//StudentController->getAllStudents();

//Trường hợp đặc biệt:http://localhost/phpmvc/index.php
//Default: c=HomeController a=index() > HomeController->index()

//Trương hợp đặc biệt:http://localhost/phpmvc/student/all
//URL rewrite > Đường dẫn thân thiện > SEO
//student/all ? c=StudentController & a=getAll


//CHỐT: Mọi URL có dạng http://localhost/phpmvc/index.php?c=x&a=y
//Method: GET - key=value

//if(isset($_GET['c'])){
//    $controller = $_GET['c'];
//}else{
//    $controller = 'home';
//}

$controller = isset($_GET['c'])?$_GET['c']:'home';
$action     = isset($_GET['a'])?$_GET['a']:'index';

//echo $controller.'--'.$action;
$controller = ucfirst($controller); //Chuyển kí tự đầu sang in hoa: home > Home

$controller = $controller."Controller"; //Home > HomeController
$path = "controllers/".$controller.".php"; //HomeController > controllers/HomeController.php
//echo $path;
if(!file_exists($path)){
    die("Request not found. Check your path");
}
include "$path";
$myController = new $controller();
if (method_exists($myController, $action)) {
    $myController->$action();
} else {
    echo "$action does not exist in $controller class";
}

#include "controllers/HomeController.php";
