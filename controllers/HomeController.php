<?php
class HomeController{
    public function index(){
        //Có làm việc với Service/Model ko?
        include "services/StudentService.php";
        $stdService = new StudentService();
        $students = $stdService->getAllStudents();
        //Hiển thị ra view nào
        include "views/home/index.php";
    }

    public function detail(){
        $data = "Detail from ....";
        include "views/home/detail.php";
    }
}