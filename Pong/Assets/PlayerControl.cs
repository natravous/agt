using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    public float speed = 10f;

    public float yBoundary = 9f;

    private Rigidbody2D rigidBody2D;

    private int score;

    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

    // Untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Dapatkan kecepatan raket sekarang.
        Vector2 velocity = rigidBody2D.velocity;

        // Dapatkan posisi raket sekarang.
        Vector3 position = transform.position;

        if (Input.GetKey(upButton)) // Jika pemain menekan tombol ke atas, beri kecepatan positif ke komponen y (ke atas).
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downButton)) // Jika pemain menekan tombol ke bawah, beri kecepatan negatif ke komponen y (ke bawah).
        {
            velocity.y = -speed;
        }
        else // Jika pemain tidak menekan tombol apa-apa, kecepatannya nol.
        {
            velocity.y = 0f;
        }

        // Masukkan kembali kecepatannya ke rigidBody2D.
        rigidBody2D.velocity = velocity;

        if(position.y > yBoundary) // Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas tersebut.
        {
            position.y = yBoundary;
        }
        else if (position.y < -yBoundary) // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.
        {
            position.y = -yBoundary;
        }

        // Masukkan kembali posisinya ke transform.
        transform.position = position;
    }

    // Menaikkan skor sebanyak 1 poin
    public void IncrementalScore()
    {
        score++;
    }

    // Mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    // Mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }

    // Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

}
