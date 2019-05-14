import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../services/empservice.service';

@Component({
    templateUrl: './adddepartment.component.html'
})

export class AddDepartmenComponent implements OnInit {
    departmenForm: FormGroup;
    title: string = "Create";
    departmentId: number;
    errorMessage: any;

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _employeeService: EmployeeService, private _router: Router) {
        if (this._avRoute.snapshot.params["id"]) {
            this.departmentId = this._avRoute.snapshot.params["id"];
        }

        this.departmenForm = this._fb.group({
            departmentId: 0,
            name: ['', [Validators.required]]
        })
    }

    ngOnInit() {

    }

    save() {

        if (!this.departmenForm.valid) {
            return;
        }

        this._employeeService.addDepartment(this.departmenForm.value)
            .subscribe((data) => {
                this._router.navigate(['/fetch-employee']);
            }, error => this.errorMessage = error)
    }

    get name() { return this.departmenForm.get('departmentName'); }
}  