# Marketing web application with implemented security mechanisms

## About

This project is a part of the **Security in the e-business systems** course. The goal of this project is to implement various security mechanisms into the marketing web application.

## Implemented Security Mechanisms

- **User Registration and Login** with implemented salted hashing mechanism when storing passwords in the database.

- **User account verification** - System administrator can accept/decline user registration. If accepted, user recieves an email with a verification link which they need to access to verify their account.

- **Access and Refresh tokens** - On successful login, the user receives JWT access token and a refresh token cookie which serves to re-authenticate user when the JWT expires. JWT expires after 15 minutes, and refresh token expires after 7 days.

- **Passwordless login** - User can request passwordless login. System generates OTP token embeded into the link which is sent to the user via the email. When clicked, authentification request is automatically sent and the user is logged in.

- **Two-factor Authentification** - User can activate 2FA by scaning QR code on the app with the TOTP authentificator apps like Google or Microsoft Authenticator. If activated, on successful login, user is required to enter the TOTP generated in the authenticator app.

- **CAPTCHA** - When signing in, captcha score is returned from the Google API which determins if the user is robot.

- **HTTPS** - The application is configured to use a secure version of the HTTP with created self-signed certificate

- **Encrption of the sensitive data** - Following the GDPR, sensitive user data is symetricaly encripted in the database.

- **Logging, monitoring and system warnings** - System is implemented to log important actions and it warns administratoris in the case of errors.

- **Pentesting and dependency analisys** - App is tested against the OWASP 10 security risks and the dependecy analisys has been run on it to check the amount of vulnrable dependencies.

## Technologies

- **Languages/Frameworks:** DotNetCore, Angular, Rust
- **Database management system:** PostgreSql
- **External services:** Google ReCaptchav3

## Authors

- Nikola Simić RA 32/2020
- Anastasija Radić RA 40/2020
- Jelena Vujović RA 80/2020
- Srđan Petronijević RA 201/2020
