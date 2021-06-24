(function () {

 
    const bomTypeList= [
        //{ 'Code': 'N', 'Name': 'None' },
        { 'Code': 'A', 'Name': 'Assembly' },
        { 'Code': 'S', 'Name': 'Sales' },
        { 'Code': 'P', 'Name': 'Product' },
        { 'Code': 'T', 'Name': 'Template' },
    ];

    angular.module('PRODUCT.MODELS', [])
        .factory('BOM.Model', function () {
            return {
                'BOMTypeList': bomTypeList
            };
        })
}());