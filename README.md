<p align="center">
  <a href="https://angularspree.firebaseapp.com/" target='_blank'>
    <img alt="AngualreSpree Logo" title="AngularSpree Logo" src="https://res.cloudinary.com/mally/image/upload/v1490186051/Angular_spree_hqgwtq.png" width="200">
  </a>
</p>

<p align="center">
  AngularSpree Plug and play frontend application for SPREE E-Commerce API built with ❤️ using Angular6, Redux, Observables & ImmutableJs.
</p>

<p align="center">
  <a href="https://angularspree.firebaseapp.com/" target='_blank'>Check demo</a> | <a href="https://aviabird.github.io/angularspree/" target="_blank">Docs </a>
</p>

<p align="center">
  <a href="/CONTRIBUTING.md" target='_blank'><img alt="PRs Welcome" src="https://img.shields.io/badge/PRs-welcome-brightgreen.svg"></a>
  <a href="https://gitter.im/aviabird/angularspree"><img src="https://badges.gitter.im/aviabird/angularspree.svg"/></a>
  <a href="https://www.pivotaltracker.com/n/projects/2165435" target='_blank'><img alt="Pivotal Project page" src="https://res.cloudinary.com/zeus999/image/upload/c_limit,h_1041,w_1487/v1486457388/Yatrum%20Logo/pt-badge_ss3dyt.svg"></a>
</p>

<p align="center">
  <a href="https://teracommerce.in" target="_blank"><img alt="TeraCommerce Bundle" src="https://res.cloudinary.com/ashish173/image/upload/v1506115865/Full_E-COMMERCE_BUNDL_3_km1yzz.jpg"></a>
</p>


### :rocket: **Progressive Web App:** [Lighthouse](https://github.com/GoogleChrome/lighthouse) score of __95/100__.

## What is AngularSpree?

AngularSpree is an open source Angular(6.x+) front-end application for [Spree Commerce](https://github.com/spree/spree). 
**It's free and always will be**. 

**Bootstrap 4 Compatible**

Go ahead use it the way you want to or let us know at `hello@aviabird.com` if you need any help with this project.

### Quick Links
[Gitter](https://gitter.im/aviabird/angularspree) | [Contributing](https://github.com/aviabird/angularspree/blob/master/CONTRIBUTING.md) | [Wiki](https://github.com/aviabird/angularspree/wiki) | 
|---|---|---|

## Why did we build it?

We have been working with Spree for very long time, making products for a lot of clients. There was one pattern we noticed in what the clients always asked for. They were comfortable using spree for the backend [API](http://guides.spreecommerce.org/api/) but not for the front-end. These requests have been very consistent with so many awesome [front-end framework](https://github.com/showcases/front-end-javascript-frameworks) around.

When Angular team released the beta version in March last year we knew that angular was going to be a big player soon.
We decided to give it a try. Hence, AngularSpree was born as a front-end framework for the most awesome backend api for E-Commerce out there.

🔥🔥🔥🔥🔥🔥🔥🔥🔥

If you are looking to build a project similar to this one with all the ready-made setup, then I highly encourage you to look at this [starter seed project](https://github.com/aviabird/angular-seed) by [Aviabird Team](https://aviabird.com).

**[AngularSeed](https://github.com/aviabird/angular-seed) is a Plug and play Seed project built with ❤️ using Angular 6+, Redux/ngrx-store 6, Observables & ImmutableJs. We are commited to keeping this project upto date with all the latest versions of all the libs and components.**

🔥🔥🔥🔥🔥🔥🔥🔥🔥

            //check TenancyReference
            // () => get from ibsService or cloudVoid if TenancyReference cannot be found

            // if tenancyReference cannot found
                // check IsCurrentTenant by call function
                    // if found ()=> call transferibs
                    // else ()=> call signupibs
                             // get tenants from crm ()=>
                            // createnewtanant()=> createnewtenant()=>
                // if the result from above deletedlist has data , record error
                // if the result not create tenancyRef or not valid tenancyRef, call error

            //if result correct, call function to save the signup reseult
