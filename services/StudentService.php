<?php
include "models/Student.php";
class StudentService{
    //Mang la CSDL
    private $listOfStudents;

    public function __construct(){
        //Khoi tao mang voi 1 danh sach SV minh hoa tao san
        $std01 = new Student("205106111",'Nguyen Van A', '24/5/2000');
        $std02 = new Student("205106112",'Nguyen Van B', '24/5/2000');
        $std03 = new Student("205106113",'Nguyen Van B', '24/5/2000');
        $this->listOfStudents[] = $std01; //cách 1: thêm phần tử vào mảng
        array_push($this->listOfStudents, $std02, $std03); //cách 2: thêm phần tử vào mảng
    }

    public function getAllStudents(){
        return $this->listOfStudents;
    }
}