import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

import { AccountService } from '../../services/account.service';
import { BankAccount } from '../../models/bank-account.model';

@Component({
    selector: 'user-bank-accounts',
    templateUrl: './bank-accounts.component.html',
    styleUrls: ['./bank-accounts.component.css']
})
/** bank-accounts component*/
export class BankAccountsComponent implements OnInit, AfterViewInit {

    bankAccounts: BankAccount[] = [];
    rows: any[];
    columns: any[];

    /** bank-accounts ctor */
    constructor(private accountService : AccountService ) {

    }

    ngOnInit(): void {

        this.loadData();
    }

    ngAfterViewInit(): void {
    }

    loadData() {

        this.accountService.getMyBankAccounts()
            .subscribe(
            results => function ()
            {
                this.bankAccounts = results;
            },
            error => function () {
                
                    //this.alertService.showStickyMessage("Load Error", `Unable to retrieve users from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
                    //    MessageSeverity.error, error);
                });
    }
    
}