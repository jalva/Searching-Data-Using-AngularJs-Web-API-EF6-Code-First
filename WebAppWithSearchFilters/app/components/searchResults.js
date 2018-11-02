app.component('searchResults', {
    templateUrl: 'views/searchResults.html',
    bindings: {
        columns: '<',
        results: '<',
        collectionName: '@'
    },
    controller: function ($scope, $location) {
        var $ctrl = this;

        this.$onInit = function () {
            this.columns = this.columns.data;
            selectedCol = this.columns[0];
        }
        
        var selectedCol;
        
        this.toggleColSort = function (col) {
            var orderDesc = !selectedCol.orderDesc;
            if (selectedCol) {
                selectedCol.orderDesc = '';
                selectedCol.orderBySelected = false;
            }
            selectedCol = col;
            selectedCol.orderBySelected = true;
            selectedCol.orderDesc = orderDesc;

            $location.search('orderBy', selectedCol.orderBy);
            $location.search('orderDesc', selectedCol.orderDesc.toString());
        }


        $scope.$watch(function () { return $location.search()['orderBy']; }, function (newValue, oldValue) {
            var isDesc = $location.search()['orderDesc'] == 'true';
            updateFromQs(newValue, isDesc);
        });
        
        function updateFromQs(orderBy, orderDesc) {
            for (var i = 0; i < $ctrl.columns.length; i++) {
                if ($ctrl.columns[i].orderBy == orderBy) {
                    $ctrl.columns[i].orderBySelected = true;
                    $ctrl.columns[i].orderDesc = orderDesc;
                    selectedCol = $ctrl.columns[i];
                    break;
                }
            }
        }
    }

});
