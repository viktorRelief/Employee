import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../services/empservice.service'

@Component({
    templateUrl: './fetchemployee.component.html'
})

export class FetchEmployeeComponent {
    public empData: object;
    public page: number = 1;

    constructor(public http: Http, private _router: Router, private _employeeService: EmployeeService) {    
    }

    ngOnInit() {
        this.getEmployees();
    }

    getEmployees() {
        this._employeeService.getEmployees(this.page).subscribe(
            data => this.empData = data
        );
    }

    confirmDelete(id) {
        this._employeeService.confirmDeleteEmployee(id).subscribe(
            data => this.getEmployees()
        );

        if (this.empData) {
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

    move(page) {
        this._employeeService.getEmployees(page).subscribe(
            data => this.empData = data
        );
    }
}

interface EmployeeData {
    employee: Employee[];
    pageViewModel: object; 
}

interface Employee {
    id: number;
    employeeLogin: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    email: string;
    homeAddress: string;
    department: object;
} 