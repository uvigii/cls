<?php
namespace MF\RingBundle\Entity;
use Doctrine\ORM\Mapping as ORM;

/**
 * @ORM\Table(name="ring")
 * @ORM\Entity()
 */

class Ring 
{
   /**
     * @ORM\Id
     * @ORM\Column(type="integer")
     * @ORM\GeneratedValue(strategy="AUTO")
     */
    protected $id;
            
    /**
     * @ORM\Column(type="string", length=16, nullable=false)
     */
    protected $tel;  
    
     /**
     * @ORM\Column(type="datetime")
     */
    protected $logedat;
        

    /**
     * Get id
     *
     * @return integer 
     */
    public function getId()
    {
        return $this->id;
    }

    /**
     * Set tel
     *
     * @param string $tel
     */
    public function setTel($tel)
    {
        $this->tel = $tel;
    }

    /**
     * Get tel
     *
     * @return string 
     */
    public function getTel()
    {
        return $this->tel;
    }

    /**
     * Set logedat
     *
     * @param datetime $logedat
     */
    public function setLogedat($logedat)
    {
        $this->logedat = $logedat;
    }

    /**
     * Get logedat
     *
     * @return datetime 
     */
    public function getLogedat()
    {
        return $this->logedat;
    }
}