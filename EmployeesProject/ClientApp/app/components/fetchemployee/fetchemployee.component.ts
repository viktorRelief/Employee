import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../services/empservice.service'

@Component({
    templateUrl: './fetchemployee.component.html'
})

export class FetchEmployeeComponent {
    public empList: EmployeeData[];
    public confirmedEmployee: EmployeeData[];

    constructor(public http: Http, private _router: Router, private _employeeService: EmployeeService) {
        this.getEmployees();
    }

    getEmployees() {
        this._employeeService.getEmployees().subscribe(
            data => this.empList = data
        );
    }

    confirmDelete(id) {
        this._employeeService.confirmDeleteEmployee(id).subscribe(
            data => this.getEmployees()
        );

        if (this.empList) {
            this.delete(id);
        }
    }

    delete(id) {
        var ans = confirm("Do you want to delete employee with Id: " + id);     

        if (ans) {
            this._employeeService.deleteEmployee(id).subscribe((data) => {
                this.getEmployees();
            }, error => console.error(error))
        }
    }
}

interface EmployeeData {
    id: number;
    employeeLogin: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    email: string;
    homeAddress: string;
    department: object;
} 