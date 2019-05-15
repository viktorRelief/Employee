import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { DepartmentService } from '../../services/depservice.service';

@Component({
    templateUrl: './adddepartment.component.html'
})

export class AddDepartmenComponent implements OnInit {
    departmenForm: FormGroup;
    title: string = "Create";
    departmentId: number;
    errorMessage: any;
    departmentList: Array<any> = [];
    departmentAlreadyExist: boolean;

    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,
        private _departmentService: DepartmentService, private _router: Router) {
        if (this._avRoute.snapshot.params["id"]) {
            this.departmentId = this._avRoute.snapshot.params["id"];
        }

        this.departmenForm = this._fb.group({
            departmentId: 0,
            name: ['', [Validators.required]]
        })
    }

    ngOnInit() {
        this._departmentService.getDepartmentList().subscribe(
            data => this.departmentList = data
        )
    }

    save() {

        for (var i = 0; i < this.departmentList.length; i++) {
            if (this.departmentList[i].name === this.departmenForm.value.name) {
                this.departmentAlreadyExist = true;
                break;
            }
            else {
                this.departmentAlreadyExist = false;
            }
        }

        if (!this.departmenForm.valid || this.departmentAlreadyExist) {
            return;
        }

        this._departmentService.addDepartment(this.departmenForm.value)
            .subscribe((data) => {
                this._router.navigate(['/fetch-employee']);
            }, error => this.errorMessage = error)
    }

    get name() { return this.departmenForm.get('name'); }
}  