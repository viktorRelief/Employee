﻿import { Component, Inject } from '@angular/core';
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

    confirmDelete(employeeID) {
        this._employeeService.confirmDeleteEmployee(employeeID).subscribe(
            (data) => {
                this.getEmployees();
            }, error => console.error(error)
        );

        console.log(this.empList);

        if (this.empList) {
            this.delete(employeeID);
        }
    }

    delete(employeeID) {
        var ans = confirm("Do you want to delete employee with Id: " + employeeID);     

        if (ans) {
            this._employeeService.deleteEmployee(employeeID).subscribe((data) => {
                this.getEmployees();
            }, error => console.error(error))
        }
    }
}

interface EmployeeData {
    employeeId: number;
    employeeLogin: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    email: string;
    homeAddress: string;
    department: string;
} 