# Import secrets for the BSEP api backend

1. Request secrets.json file from [Nikola Simic](https://github.com/dXellor)
2. Place secrets.json into the scripts subdirectory of this repository
3. Run `import_secrets` script (WIN for windows, UNIX for MacOS and Linux)

# rate_limit script
1. install Rust by running the following command in your terminal: curl https://sh.rustup.rs -sSf | sh
2. navigate to rate_limiter directory: cd bsep-ra-2024-kt2-tim-12/scripts/rate_limiter
3. run the script: cargo run