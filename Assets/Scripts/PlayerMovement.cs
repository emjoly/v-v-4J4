using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
 
public class PlayerMovement : MonoBehaviour
{
    // Variables pour le mouvement du joueur

    // Acquérir le Rigidbody2D et le BoxCollider2D du joueur
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    public Animator animator;
    public SpriteRenderer playerSprite;
    // LayerMask pour définir les layers sur lesquels le joueur peut sauter
    [SerializeField] private LayerMask JumpableGround;
    // Variables pour le mouvement du joueur
    [SerializeField] private float moveSpeed = 7f; // Vitesse de déplacement du joueur
    [SerializeField] private float maxFallSpeed = 20f; // Vitesse maximale de chute du joueur
    [SerializeField] private float gravityMultiplier = 2f; // Multiplicateur de gravité

    // Variables pour les dashs
    private bool canDash = true; // Flag pour vérifier si le joueur peut dash
    private bool isDashing; // Flag pour vérifier si le joueur est en train de dash
    private float dashSpeed = 20f; // Vitesse de dash
    private float dashTime = 0.2f; // Durée de dash
    private float dashCooldown = 0.2f; // Cooldown de dash (permet au joueur de faire un dash toutes les 0.2 secondes)
    private bool hasDashedInAir = false; // Flag pour vérifier si le joueur a déjà dashé dans les airs
    private float groundDashCooldown = 0f; // Cooldown de dash au sol

    // Variables pour les sauts et double sauts
    [SerializeField] private float jumpTime; // Temps de saut
    [SerializeField] private float jumpForce = 14f; // Force de saut du joueur
    private float jumpTimeCounter; // Compteur de temps de saut
    private bool isJumping; // Flag pour vérifier si le joueur est en train de sauter
    private int doubleJump; // Compteur de double saut
    [SerializeField] private int doubleJumpV; // Valeur du double saut (combien de sauts on veut autoriser)
    [SerializeField] private int doubleJumpF; // Force du double saut
    // Variables pour le apex point du jump (le point le plus haut du saut)
    private float _jumpApexThreshold = 0.7f; // Le apex point du saut 0.7 = 70% de la hauteur du saut
    private float _apexBonus = 13f; // Bonus de vitesse à appliquer au apex point (donne un effet de flottement au joueur)
    // Variables pour le slam
    [SerializeField] private float slamForce = 30f;
    public bool isSlaming = false;
    bool isFalling = false;

    // Variables de vie du joueur
    public int maxHealth = 100;
    public int currentHealth;
    public HealtBar healthBar;

    // Autres variables
    //À implémenter plus tard (pour activer le double jump)
    private bool hasPickedUpItem = false;

    public Transform respawnPoint; // Point de respawn du joueur
    public PlayerCombat playerCombat;

    // Variables sons
    public AudioClip SonMarche;
    public AudioClip SonBlesse;
    public AudioClip SonSaut;
    public AudioClip SonMort;

    private string SceneCourante;

    // Si le joueur est mort ou blesse
    public bool isDead = false;
    public bool isBlesse = false;

    

    void Start()
    {
        // Acquérir le Rigidbody2D et le BoxCollider2D du joueur
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        // Initialiser les variables de vie du joueur
        currentHealth = maxHealth;
        // Initialiser la barre de vie du joueur
        healthBar.SetMaxHealth(maxHealth);

        DontDestroyOnLoad(this.gameObject); // Ne pas détruire le joueur lors du changement de scène
    }

    void FixedUpdate()
    {
    }
    void Update()
    
    {

        SceneCourante = SceneManager.GetActiveScene().name;
        if (!isDead && !isBlesse)
        {
            
            if (SceneCourante == "Lvl2")
            {
                if (!isSlaming)
                {
                    if (Input.GetButtonDown("Slam") && !IsGrounded())
                    {
                        StartCoroutine(SlamThroughPlatforms());
                        return;
                    }
                }
            }

            // Déplacer le joueur avec le clavier(getAxisRaw pour éviter l'accélération du joueur)
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            if (IsGrounded() && directionX != 0)
            {
                animator.SetBool("Marche", true);
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().PlayOneShot(SonMarche); //je comprend pas ca, le son de marche joue pas
                }
            }
            else
            {
                animator.SetBool("Marche", false);
            }


            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            // Si le joueur est en train de dash, ne pas exécuter le code suivant
            if (isDashing)
            {
                return;
            }

            // Calcul du bonus de vitesse en fonction de la hauteur du saut
            float _apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(rb.velocity.y));
            // initialize apexBonus à 0
            float apexBonus = 0f;

            // Si le joueur est au apex point (le point le plus haut) du saut, appliquer le apexBonus
            if (_apexPoint > 0 && !IsGrounded())
            {
                // Calculer le apexBonus en fonction de la direction du saut
                apexBonus = Mathf.Sign(rb.velocity.y) * _apexBonus * (1 - Mathf.Abs(_apexPoint - 0.5f) * 2);
            }

            // Appliquer le apexBonus à la velocité verticale du joueur
            rb.velocity += Vector2.up * apexBonus * Time.deltaTime;

            // Calculer la vitesse horizontale actuelle du joueur = vitesse de déplacement + apexBonus
            float _currentHorizontalSpeed = moveSpeed + apexBonus;

            // Déplacer le joueur horizontalement
            rb.velocity = new Vector2(directionX * _currentHorizontalSpeed, rb.velocity.y);


            if (directionX < 0 && playerSprite.flipX)
            {
                playerSprite.flipX = false; // Transform le scale du joueur pour ensuite flip le sprite
                playerCombat.FlipPlayer(); // Pour flip le attack point aussi
            }
            // Si la direction est positive, reset le scale du joueur
            else if (directionX > 0 && !playerSprite.flipX)
            {
                playerSprite.flipX = true; // Reset le scale original ù du joueur
                playerCombat.FlipPlayer();
            }


            // Si le joueur appuie sur la touche Jump
            if (Input.GetButtonDown("Jump"))
            {
                // Si le joueur est au sol
                if (IsGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpTimeCounter = jumpTime; // Initialiser le compteur de temps de saut
                    isJumping = true; // Le joueur est en train de sauter
                    hasDashedInAir = false; // Reset le flag de dash dans les airs
                    animator.SetBool("Monte", true); // Set "Monte" parameter to true when jumping
                    GetComponent<AudioSource>().PlayOneShot(SonSaut);
                }
                // Si le joueur n'est pas au sol et n'a pas encore commencé le double saut
                else if (!isJumping && doubleJump > 0)
                {
                    // Initialiser le double saut
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    doubleJump--; // Décrémenter le double jump
                    animator.SetBool("Monte", true); // Set "Monte" parameter to true when jumping
                    GetComponent<AudioSource>().PlayOneShot(SonSaut);
                }
            }

            // Si le joueur appuie sur la touche Jump et est en train de sauter
            if (Input.GetButton("Jump") && isJumping)
            {
                // Si le compteur de temps de saut n'est pas écoulé
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Appliquer une force de saut
                    animator.SetTrigger("Monte2"); // Jouer l'animation de double saut
                    jumpTimeCounter -= Time.deltaTime; // Décrémenter le compteur de temps de saut
                }
                else
                {
                    // Le joueur a fini de sauter
                    isJumping = false;
                    animator.SetBool("Monte", false); // Set "Monte" parameter to false once jump finishes
                }
            }

            // Si le joueur relâche la touche Jump
            if (Input.GetButtonUp("Jump"))
            {
                // Pour ne pas sauter indéfiniment
                isJumping = false;
                animator.SetBool("Monte", false);
            }
            // Si le joueur appuie sur la touche Dash
            if (Input.GetButtonDown("Dash"))
            {
                // Si le joueur n'est pas au sol et n'est pas en train de dash et peut dash
                if (!IsGrounded() && canDash && !hasDashedInAir)
                {

                    StartCoroutine(Dash()); // Lancer la coroutine Dash
                    canDash = false; // Mettre le flag de dash à false (ne peut pas redash dans les airs)
                    hasDashedInAir = true; // Mettre le flag de dash dans les airs à true
                }
                // Si le joueur est au sol et le cooldown de dash au sol est écoulé
                else if (IsGrounded() && Time.time >= groundDashCooldown)
                {
                    StartCoroutine(Dash()); // Lancer la coroutine Dash (pour pouvoir dasher au sol)
                    groundDashCooldown = Time.time + 1f; // Mettre le cooldown de dash au sol à 2 secondes
                }
            }
            // && !isAttacking pour quil attaque a la place de tomber mais lanim est dans lautre script

        // Check if the player is falling
        if (rb.velocity.y < 0)
        {
            // Set the "Tombe" parameter to true and perform falling actions
            animator.SetBool("Tombe", true);
            isFalling = true;
                                // Appliquer une gravité plus forte
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;
                    // Limiter la vitesse de chute maximale
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));

            // Check if the player initiated a slam while falling
            if (Input.GetButtonDown("Slam") && !IsGrounded() && !isSlaming)
            {
                // Override the "Tombe" animation and play the slam animation immediately
                animator.SetBool("Tombe", false); // Disable the falling animation
                StartCoroutine(SlamThroughPlatforms()); // Play the slam animation
                return; // Return to avoid executing further movement logic
            }
        }
        else if (rb.velocity.y >= 0)
        {
            // Reset the "Tombe" parameter and falling flag when player is not falling
            animator.SetBool("Tombe", false);
            isFalling = false;
        }

            
        }
    }


    // Fonction pour prendre des dégats
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            // Réduire la vie du joueur par le montant de dégats
            currentHealth -= damage;
            // Mettre à jour la barre de vie du joueur
            healthBar.SetHealth(currentHealth);
            animator.SetTrigger("Mal");
            animator.SetBool("Tombe", false);
            animator.SetBool("Monte", false);
            isBlesse = true;
            GetComponent<AudioSource>().PlayOneShot(SonBlesse);
            StartCoroutine(BlesseBack2False(1.0f)); // freeze quand il est blessé
        }
    }

    private IEnumerator BlesseBack2False(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBlesse = false;
    }

    // Fonction pour déterminer si le joueur est au sol
    private bool IsGrounded()
    {
        // Regarder si le joueur est en collision avec des layers spécifiés dans le LayerMask JumpableGround
        bool groundedOnJumpableGround = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, JumpableGround);

        // Regarder si le joueur est en collision avec des layers spécifiés dans le LayerMask BrisPlateforme
        bool groundedOnBreakablePlatform = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, LayerMask.GetMask("BrisPlateforme"));

        // Le joueur est au sol s'il est en collision avec un des layers spécifiés
        bool grounded = groundedOnJumpableGround || groundedOnBreakablePlatform;

        // Retourner si le joueur est au sol
        return grounded;
    }

    // Fonction pour ramasser un item
    public void PickupItem()
    {
        hasPickedUpItem = true; // Le joueur a ramassé un item
    }

    // Fonction coroutine pour le dash
    private IEnumerator Dash()
    {
        isDashing = true; // Le joueur est en train de dash (prévention de spam de dash)
        canDash = false; // Le joueur ne peut pas dash (doit mettre au debut de la coroutine (Prévention de spam de dash))
        float originalGravity = rb.gravityScale; // Sauvegarder la gravité originale du joueur
        rb.gravityScale = 0; // Mettre la gravité du joueur à 0
        animator.SetTrigger("Fonce"); // Jouer l'animation de dash
        // Determine the direction of the dash
        float dashDirection = playerSprite.flipX ? 1 : -1;

        rb.velocity = new Vector2(dashDirection * dashSpeed, 0); // Appliquer une vélocité de dash

        yield return new WaitForSeconds(dashTime); // Attendre la durée de dash
        rb.gravityScale = originalGravity; // Remettre la gravité originale du joueur
        isDashing = false; // Le joueur n'est plus en train de dash (prévention de spam de dash)
        yield return new WaitForSeconds(dashCooldown); // Attendre le cooldown de dash
        canDash = true; // Le joueur peut dash à nouveau (Doit mettre à la fin d'une coroutine (Prévention de spam de dash))
    }


    // Fonction pour le double saut
    void ExtraJump()
    {
        // Si le joueur appuie sur la touche Jump et a encore des double jumps et n'est pas au sol
        if (Input.GetButtonDown("Jump") && doubleJump > 0 && !IsGrounded())
        {
            rb.velocity = Vector2.up * doubleJumpF; // Appliquer une force de double saut
            doubleJump--; // Décrémenter le double jump
        }
        // Si le joueur appuie sur la touche Jump et est au sol
        else if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = Vector2.up * jumpForce; // Appliquer une force de saut
            doubleJump = doubleJumpV; // Réinitialiser le double jump
        }
    }

private IEnumerator SlamThroughPlatforms()
{
    isSlaming = true; // Set slam flag to true
    // Disable collisions with breakable platforms
    Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("BrisPlateforme"), true);

    // Apply slam force downwards
    rb.velocity = Vector2.down * slamForce;
    animator.SetTrigger("Slam"); // Trigger the slam animation

    // Wait for the duration of the slam
    yield return new WaitForSeconds(0.2f);

    // Re-enable collisions with breakable platforms
    Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("BrisPlateforme"), false);

    isSlaming = false; // Reset slam flag
}





    void OnCollisionEnter2D(Collision2D collision)
    {

        // Si le joueur entre en collision avec un ennemi qui a le tag Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10); // Réduit la vie du joueur de 10
            // faudrait ajouter que sil attaque il perd pas de vie et linsecte recul un peu?
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reset the "Monte" parameter
            animator.SetBool("Monte", false);
            isJumping = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vie"))
        {
            // Restaurer la santé du joueur au maximum
            currentHealth = maxHealth;
            // Mettre à jour l'interface utilisateur de la barre de santé
            healthBar.SetHealth(currentHealth);
            // Détruire l'objet avec le tag "Vie"
            Destroy(collision.gameObject);
            // Ajouter un son !!!
        }
        if (collision.gameObject.CompareTag("PicSol"))
        {
            Die();
        }
    }

    void Die()
    {
        /*
         // Disable player movement and controls
         rb.velocity = Vector2.zero; // Stop player movement
         rb.gravityScale = 0; // Disable gravity
         rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
         rb.constraints = RigidbodyConstraints2D.FreezeAll;
         boxCollider.enabled = false; // Disable the collider to prevent further collisions*/
        if (isDead) return;
        isDead = true; // Set isDead to true
        // Jouer l'animation de mort
        GetComponent<Animator>().SetTrigger("Mort");
        animator.SetBool("Tombe", false);
        StartCoroutine(ReloadSceneAfterDelay(3.0f)); // Reload scene after 3 seconds
        GetComponent<AudioSource>().PlayOneShot(SonMort); // Jouer le son de mort
        // Peut-être screen gameover ?
        // mais Die est appele meme quand il touche le solPic, faudrait quil reapparait sur une plateforme mais je sais pas a quel point cest faisable
    }

    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    /* IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
 
        // Reset player state
        boxCollider.enabled = true; // Re-activer le collider
        enabled = true; // Reactiver le script
 
        transform.position = respawnPoint.position; // Téléporter le joueur au point de respawn
 
        // Peut-être à implémenter mais pour l'instant on reload la scene
        // animator.SetTrigger("Respawn");
        // if (respawnSound) audioSource.PlayOneShot(respawnSound);
    } */
}
