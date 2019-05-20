import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FetchEmployeeComponent } from '../fetchemployee/fetchemployee.component';
import { EmployeeService } from '../../services/empservice.service';
import { DepartmentService } from '../../services/depservice.service';

@Component({
    templateUrl: './addEmployee.component.html'
})

export class createemployee implements OnInit {
    employeeForm: FormGroup;
    title: string = "Create";
    id: number;
    errorMessage: any;
    departmentList: depList[];
    emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
    phonePattern = "[(][0-9]{3}[)] [0-9]{3}-[0-9]{4}";
    textOnlyPattern = "^[a-zA-Z ]*$";

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _employeeService: EmployeeService, private _departmentService: DepartmentService, private _router: Router) {
        if (this._avRoute.snapshot.params["id"]) {
            this.id = this._avRoute.snapshot.params["id"];
        }

        this.employeeForm = this._fb.group({
            id: 0,
            employeeLogin: ['', [Validators.required]],
            firstName: ['', [Validators.required, Validators.pattern(this.textOnlyPattern)]],
            lastName: ['', [Validators.required, Validators.pattern(this.textOnlyPattern)]],
            phoneNumber: ['', [Validators.required, Validators.pattern(this.phonePattern)]],
            email: ['', [Validators.required, Validators.pattern(this.emailPattern)]],
            homeAddress: ['', [Validators.required]],
            departmentId: ['', [Validators.required]]
        })
    }

    ngOnInit() {

        this._departmentService.getDepartmentList().subscribe(
            data => this.departmentList = data
        )

        if (this.id > 0) {
            this.title = "Edit";
            this._employeeService.getEmployeeById(this.id)
                .subscribe(resp => this.employeeForm.setValue(resp)
                , error => this.errorMessage = error);
        }

    }

    save() {

        if (!this.employeeForm.valid) {
            return;
        }

        if (this.title == "Create") {
            this._employeeService.saveEmployee(this.employeeForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-employee']);
                }, error => this.errorMessage = error)
        }
        else if (this.title == "Edit") {
            this._employeeService.updateEmployee(this.employeeForm.value)
                .subscribe((data) => {
                    this._router.navigate(['/fetch-employee']);
                }, error => this.errorMessage = error)
        }
    }

    cancel() {
        this._router.navigate(['/fetch-employee']);
    }

    get employeeLogin() { return this.employeeForm.get('employeeLogin'); }
    get firstName() { return this.employeeForm.get('firstName'); }
    get lastName() { return this.employeeForm.get('lastName'); }
    get phoneNumber() { return this.employeeForm.get('phoneNumber'); }
    get email() { return this.employeeForm.get('email'); }
    get homeAddress() { return this.employeeForm.get('homeAddress'); }
    get departmentId() { return this.employeeForm.get('departmentId'); }

}

interface depList {
    id: number;
    name: string;
} 