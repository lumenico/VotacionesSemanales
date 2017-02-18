<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Historico.ascx.cs" Inherits="Votaciones.Controls.Historico" %>
 
<div ng-app="appHist" >
    <div ng-controller="ctrlHist as ctrl">
        
        <div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
			<ul class="nav nav-tabs">			
				<li class="dropdown pull-right">
					 <a href="#" data-toggle="dropdown" class="dropdown-toggle">Dropdown<strong class="caret"></strong></a>
					    <ul class="dropdown-menu">
						<li>
							<a href="#culo">Action</a>
						</li>
						<li>
							<a href="#">Another action</a>
						</li>
						<li>
							<a href="#">Something else here</a>
						</li>
						<li class="divider">
						</li>
						<li>
							<a href="#">Separated link</a>
						</li>
					</ul>
				</li>
			</ul>
		</div>
	</div>
    <div >
	    <div class="row" id="culo" >
		<div class="col-md-4">
			<div class="jumbotron ">
				<h2 class="text-center">
				    Verde
				</h2>
				<p>
					<img src="/img/more.png" class="imgHistoricoHeight "/>
				</p>
				
			</div>
		</div>
		<div class="col-md-4">
		</div>
		<div class="col-md-4">
		</div>
	</div>
</div>



    </div>
</div>

</div>


<script>
    var myApp = angular.module("appHist", [])
    
    .controller("ctrlHist", ['$scope','getData', function ($scope,getData) {
        var data = { $type: "Commands", a: "pepe" };
        $scope.a = "culo";

        getData(data, function (res) {
            alert(res);
            $scope.a = JSON.parse(res);
            $scope.$apply()
        });

    }])
   .factory('getData',
         ['$http', function ($http) {
             return function (data, callback) {
                 $.post("/getter.ashx",
               { data: JSON.stringify(data) },
                   callback);
             }
         }])




     
     //
     
 </script>