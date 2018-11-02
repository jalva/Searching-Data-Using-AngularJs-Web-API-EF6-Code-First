app.component('checkboxFilter', {
    templateUrl: 'views/checkboxFilter.html',
    bindings: {
        filter: '<',
        parentFilterTerm: '='
    },
    controller: function ($filter, $scope) {
        var $ctrl = this;

        this.selectFilter = function (filter, filterTerm) {

            if (this.parentFilterTerm)
                this.parentFilterTerm.selected = this.filter.hasSelections();

            // notify parents
            $scope.$emit("filterTermSelected", [this.filter, filterTerm]);

        }

        // listen for nested filters selections (force parents to update their selection status)
        $scope.$on('filterTermSelected', function (ev, args) {
            if (ev.targetScope.$ctrl == $ctrl)
                return;
            
            // notify observers
            if ($ctrl.selectFilter) {
                var filter = args[0];
                var filterTerm = args[1];
                $ctrl.selectFilter(filter, filterTerm);
            }
        });
    }
});