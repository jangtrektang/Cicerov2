import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import {Observable} from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { User } from './../../DTO/users/user';
import { UserService } from './../../services/users/user.service';
 
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  providers: [UserService]
})
export class SignupComponent implements OnInit {
  signupForm: FormGroup;
  user = new User();

  private _validationMessages: { [id: string]: { [id: string]: string } };
  formError: { [id: string]: string };
 
  constructor(private _fb: FormBuilder, userService: UserService) {
    // Initialize error messages
    this.initializeErrors();

    // Initialize formgroup
    this.initializeFormGroup();

    // Validate when value changes
    this.signupForm.valueChanges
      .subscribe(data => this.validateSignUpForm(true));
   }

  ngOnInit() {
  }

  signUp():void {
    if(this.signupForm.dirty && this.signupForm.valid) {      
      this.mapUser(this.signupForm.value);

      console.log(this.user);
    }
    else {
      this.validateSignUpForm(false);
      this.checkPasswords();
    }
  }

  mapUser(data: any){
    this.user.username = data.username;
    this.user.password = data.password;
    this.user.firstName = data.firstName;
    this.user.lastName = data.lastName;
    this.user.email = data.email;    
  }

  validateSignUpForm(onChange : boolean) {
    for(let field in this.formError) {
      if(this.formError.hasOwnProperty(field)){
        let hasError = false;

        if(onChange) {
           hasError =this.signupForm.controls[field].dirty &&
            !this.signupForm.controls[field].valid;
        }
        else {
           hasError = !this.signupForm.controls[field].valid;
        }       
        
        this.formError[field] = '';

        if(hasError){
          for(let key in this.signupForm.controls[field].errors){
            if(this.signupForm.controls[field].errors.hasOwnProperty(key)){
              this.formError[field] += this._validationMessages[field][key] + ' ';
            }
          }
        }
      }
    }
  }

  private checkPasswords(){
    this.formError['password'] += this._validationMessages['password']['nomatch'] + ' ';
    this.formError['passwordRepeat'] += this._validationMessages['passwordRepeat']['nomatch'] + ' ';
  }

  passwordMatcher(c: AbstractControl){
    return c.get('password').value === c.get('passwordRepeat').value
      ? null : { 'nomatch': 'blaat'};
  }

  private initializeErrors(){
    this.formError =  {
      'username': '',
      'password': '',
      'passwordRepeat': '',
      'firstName': '',
      'lastName': '',
      'email': ''
    };

    this._validationMessages = {
      'username': {
        'required': 'You must enter a username.',
        'minlength': 'Your username must be at least 5 characters long.',
        'maxlength': 'Your username can only be 100 characters long.'
      },
      'password': {
        'required': 'You must enter a password.',
        'minlength': 'Your password must be at least 6 characters long.',
        'nomatch': 'Your passwords must match.'
      },
      'passwordRepeat': {
        'required': 'You must re-enter your password.',
        'minlength': 'Your password must be at least 6 characters long.',
        'nomatch': 'Your passwords must match.'
      },
      'firstName': {
        'required': 'You must enter a first name.'
      },
      'lastName': {
        'required': 'You must enter a last name.'
      },
      'email': {
        'required': 'you must enter an emailaddress.',
        'pattern': 'You must enter a valid emailadress.'
      }
    };
  }

  private initializeFormGroup(){
    this.signupForm = this._fb.group({
      'username': ['', Validators.compose([Validators.required, Validators.minLength(5), Validators.maxLength(100)])],
      'password': ['', Validators.compose([Validators.required, Validators.minLength(6)])],
      'passwordRepeat': ['', Validators.compose([Validators.required, Validators.minLength(6)])],
      'firstName': ['', Validators.required],
      'lastName': ['', Validators.required],
      'email': ['', Validators.compose([Validators.required, Validators.pattern("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")])]
    }, { validator: this.passwordMatcher});
  }
}
