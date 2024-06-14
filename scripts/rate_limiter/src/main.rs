use serde::Serialize;
use tokio::time::{sleep, Duration};
use std::sync::Arc;
use tokio::sync::Mutex;

#[derive(Serialize)]
struct ClickRequest {
    email: String,
    package: String,
}

async fn hit_endpoint(email: &str, package: &str, times: u32, client: Arc<reqwest::Client>) {
    let url = "http://localhost:5213/api/advertisement/click";

    for i in 0..times {
        let request_body = ClickRequest {
            email: email.to_string(),
            package: package.to_string(),
        };

        let response = client.post(url)
            .json(&request_body)
            .send()
            .await;

        match response {
            Ok(resp) => {
                if resp.status() == reqwest::StatusCode::TOO_MANY_REQUESTS {
                    let body = resp.text().await.unwrap();
                    println!("User {}: Rate limit hit: {}", email, body);
                    break;
                } else {
                    println!("User {}: Request {}: Success", email, i + 1);
                }
            },
            Err(e) => {
                println!("User {}: Request {}: Error - {}", email, i + 1, e);
            }
        }
        // need delay for more "readable" output in console
        sleep(Duration::from_millis(10)).await;
    }
}

#[tokio::main]
async fn main() {
    let client = Arc::new(reqwest::Client::new());

    let basic_user1_task = hit_endpoint("nikola@gmail.com", "Basic", 15, Arc::clone(&client));
    let basic_user2_task = hit_endpoint("jelena@gmail.com", "Basic", 20, Arc::clone(&client));
    let basic_user3_task = hit_endpoint("user321@gmail.com", "Basic", 10, Arc::clone(&client));

    let standard_user1_task = hit_endpoint("pseudorandomime@gmail.com", "Standard", 150, Arc::clone(&client));
    let standard_user2_task = hit_endpoint("randomime@gmail.com", "Standard", 200, Arc::clone(&client));
    // burzuj mora da bude gold
    let gold_user_task = hit_endpoint("anastasija@gmail.com", "Gold", 10_001, Arc::clone(&client));

    tokio::join!(
        basic_user1_task, basic_user2_task, basic_user3_task,
        standard_user1_task, standard_user2_task,
        gold_user_task
    );
}
