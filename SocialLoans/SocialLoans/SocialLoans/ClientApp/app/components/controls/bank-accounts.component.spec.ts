/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { BankAccountsComponent } from './bank-accounts.component';

let component: BankAccountsComponent;
let fixture: ComponentFixture<BankAccountsComponent>;

describe('bank-accounts component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ BankAccountsComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(BankAccountsComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});