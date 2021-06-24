MetronicApp.controller('ItemDetailsController', ['$scope', 'OrderId', 'PurchaseOrderCode', 'SupplierId', 'Items', '$uibModalInstance', 'OrderingService', 'NgTableParams',
    function ($scope, OrderId, PurchaseOrderCode, SupplierId, Items, $uibModalInstance, OrderingService, NgTableParams) {
    	$scope.itemDetail = [{ MasterItemId: 0, ItemCode: "", Description: "", Cost: 0, Quantity: 0 }];
    	$scope.SupplierLists = [{}];
    	$scope.itemsToShow = [{}];
    	$scope.dataToRetrieve = Items;
    	$scope.supplierId = SupplierId;

    	$scope.getSrc = function () {
    		return "/SystemReport/PurchaseOrder.aspx?orderId=" + OrderId + "&" + "supplierId=" + SupplierId + "&" + "purchaseOrderCode=" + PurchaseOrderCode;
    	}

    	$scope.supplierChange = function (supplierId) {
    		$scope.itemsToShow = [];
    		if (supplierId == null || supplierId == 0) {
    			SupplierId = 0;
    			init();

    		}
    		else {
    			OrderingService.FilterBySupplier({
    				supplierId: supplierId
    			}).then(function (data) {

    				for (var i = 0; i < data.data.length; i++) {
    					var index = $scope.dataToRetrieve.findIndex(r=>r.MasterItemDescription === data.data[i].Description && r.SupplierId == supplierId);
    					var indexCurrentSelection = $scope.itemDetail.findIndex(r=>r.Description === data.data[i].Description && r.SupplierId == supplierId);
    					$scope.itemsToShow.push(data.data[i]);
    					if (index == -1 && indexCurrentSelection == -1) {
    						$scope.itemsToShow[i].selectedItem = false;

    					}
    					else {
    						if (index != -1) {
    							$scope.itemsToShow[i].checkedIn = true;
    						}
    						$scope.itemsToShow[i].selectedItem = true;

    					}
    				}
    			})

    		}
    		$scope.mapItems = $scope.itemsToShow;
    	}
    	function init() {

    		if (SupplierId == 0 || SupplierId == null) {
    			OrderingService.GetAllItems()
                                   .then(function (data) {
                                   	$scope.itemsToShow.splice(0, 1);
                                   	for (var i = 0; i < data.data.length; i++) {
                                   		var index = $scope.dataToRetrieve.findIndex(r=>r.MasterItemDescription === data.data[i].Description && r.SupplierId == data.data[i].SupplierId);
                                   		var indexCurrentSelection = $scope.itemDetail.findIndex(r=>r.Description === data.data[i].Description && r.SupplierId == data.data[i].SupplierId);
                                   		$scope.itemsToShow.push(data.data[i]);
                                   		if (index == -1 && indexCurrentSelection == -1) {
                                   			$scope.itemsToShow[i].selectedItem = false;
                                   			$scope.itemsToShow[i].checkedIn = false;
                                   		}
                                   		else {
                                   			if (index != -1) {
                                   				$scope.itemsToShow[i].checkedIn = true;
                                   			}
                                   			$scope.itemsToShow[i].selectedItem = true;
                                   		}

                                   	}

                                   })


    		} else {

                OrderingService.FilterBySupplier({ SupplierId: SupplierId })
                        .then(function (data) {
                        	$scope.itemsToShow.splice(0, 1);

                        	for (var i = 0; i < data.data.length; i++) {
                        		var index = $scope.dataToRetrieve.findIndex(r=>r.MasterItemDescription === data.data[i].Description && r.SupplierId == SupplierId);
                        		var indexCurrentSelection = $scope.itemDetail.findIndex(r=>r.Description === data.data[i].Description && r.SupplierId == supplierId);
                        		$scope.itemsToShow.push(data.data[i]);
                        		if (index == -1 && indexCurrentSelection == -1) {
                        			$scope.itemsToShow[i].selectedItem = false;
                        		}
                        		else {
                        			if (index != -1) {
                        				$scope.itemsToShow[i].checkedIn = true;
                        			}
                        			$scope.itemsToShow[i].selectedItem = true;
                        		}
                        	}



                        })
    		}

    		$scope.mapItems = $scope.itemsToShow;
    	}
    	init();


    	$scope.itemsToInsertSelectAll = [];
    	$scope.selectAllItems = function () {

    		$scope.itemDetail = [{ MasterItemId: 0, ItemCode: "", Description: "", Cost: 0, Quantity: 0 }];
    		for (var item = 0; item <= $scope.mapItems.length - 1; item++) {
    			if ($scope.mapItems[item].checkedIn == true) {

    			}
    			else {
    				$scope.itemsToInsertSelectAll.push({
    					MasterItemId: $scope.mapItems[item].Id, ItemCode: $scope.mapItems[item].ItemCode, Description: $scope.mapItems[item].Description,
    					Cost: $scope.mapItems[item].Cost, Quantity: 1, SupplierId: $scope.mapItems[item].SupplierId
    				});
    			}
    		}
    		if ($scope.selectAll) {
    			$scope.selectAll = true;
    			for (var item = 0; item <= $scope.itemsToInsertSelectAll.length - 1; item++) {
    				$scope.itemDetail.push({
    					MasterItemId: $scope.itemsToInsertSelectAll[item].MasterItemId, ItemCode: $scope.itemsToInsertSelectAll[item].ItemCode,
    					Description: $scope.itemsToInsertSelectAll[item].Description,
    					Cost: $scope.itemsToInsertSelectAll[item].Cost, Quantity: 1, SupplierId: $scope.itemsToInsertSelectAll[item].SupplierId
    				});
    			}


    		}
    		else {
    			$scope.selectAll = false;
    		}

    	}


    	$scope.searchProduct = function (product) {
    		OrderingService.SearchProduct({ longDescription: product, supplierId: $scope.supplierId })
            .then(function (data) {
            	$scope.itemsToShow = [];
            	for (var i = 0; i < data.data.length; i++) {
            		var index = $scope.dataToRetrieve.findIndex(r=>r.MasterItemDescription === data.data[i].Description && r.SupplierId == data.data[i].SupplierId);
            		var indexCurrentSelection = $scope.itemDetail.findIndex(r=>r.Description === data.data[i].Description && r.SupplierId == data.data[i].SupplierId);
            		$scope.itemsToShow.push(data.data[i]);
            		if (index == -1 && indexCurrentSelection == -1) {
            			$scope.itemsToShow[i].selectedItem = false;
            		}
            		else {
            			if (index != -1) {
            				$scope.itemsToShow[i].checkedIn = true;
            			}
            			$scope.itemsToShow[i].selectedItem = true;
            		}
            	}
            	$scope.mapItems = $scope.itemsToShow;
            });
    	}

        $scope.getCost = function () {
    		$uibModalInstance.close($scope.itemDetail);
    	};
    	$scope.addToItemDblClck = function (row) {

    		var index = $scope.dataToRetrieve.findIndex(r=>r.MasterItemDescription === row.Description && r.SupplierId == row.SupplierId);
    		if (index == -1) {
    			$scope.selectedItem(row);
    			$scope.getCost();
    		}
    		else {
    			alert("bawal");
    		}


    	}

    	$scope.selectedItem = function (row) {

    		var index = $scope.itemDetail.findIndex(r=>r.Description === row.Description && r.SupplierId == row.SupplierId);
    		if (index == -1) {
    			row.selectedItem = true;
    			$scope.itemDetail.push({ SupplierId: row.SupplierId, MasterItemId: row.Id, ItemCode: row.ItemCode, Description: row.Description,BrandId:row.BrandId, BrandName:row.BrandName, Cost: row.Cost, Quantity: 1, selectedItem: true });
    		}
    		else {
    			row.selectedItem = false;
    			$scope.itemDetail.splice(index, 1);
    		}

    	};


    	$scope.getAllSuppliers = function init() {
    		OrderingService.GetSuppliers()
                .then(function (data) {
                	$scope.mapSupplier = data.data;
                },
                function (error /*Error event should handle here*/) {

                },
                function (data /*Notify event should handle here*/) {
                });
    	}
    	$scope.getAllSuppliers();

    	$scope.cancel = function () {
    		$uibModalInstance.dismiss('cancel');
    	};
    }]);