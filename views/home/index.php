<!doctype html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport"
          content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Homepage</title>
</head>
<body>
<!--    Lấy ra danh sách SV từ đâu đó và hiển thị ở đây -->
<?php
    foreach($students as $std){
        echo "<p>{$std->getFullname()}</p>";
    }
?>
</body>
</html>